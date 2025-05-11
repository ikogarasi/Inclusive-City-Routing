using AutoMapper;
using InclusiveCity.Application.Dto;
using InclusiveCity.Contracts.Dto;
using InclusiveCity.Domain.Entities;
using InclusiveCity.Domain.Interfaces.Repositories;
using InclusiveCity.Domain.Interfaces.Services;
using MediatR;

namespace InclusiveCity.Application.Features.Queries.GetStructures
{
    public class GetStructuresQueryHandler : IRequestHandler<GetStructuresQuery, GetStructuresDto>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IStructureImageRepository _structureImageRepository;
        private readonly IOverpassApiService _overpassApiService;
        private readonly IMapper _mapper;

        public GetStructuresQueryHandler(
            IReviewRepository reviewRepository,
            IRatingRepository ratingRepository,
            IOverpassApiService overpassApiService,
            IStructureImageRepository structureImageRepository,
            IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _ratingRepository = ratingRepository;
            _overpassApiService = overpassApiService;
            _structureImageRepository = structureImageRepository;
            _mapper = mapper;
        }

        public async Task<GetStructuresDto> Handle(GetStructuresQuery request, CancellationToken cancellationToken)
        {
            var requestDto = _mapper.Map<OverpassRequestDto>(request);
            var overpassResponseDto = await _overpassApiService.GetStructures(requestDto);
            var structureDto = _mapper.Map<GetStructuresDto>(overpassResponseDto);

            var structureIds = overpassResponseDto.Elements.Select(i => i.Id).ToList();

            var ratingMap = new Dictionary<long, OsmRating>();
            var reviewMap = new Dictionary<long, List<OsmReview>>();
            var structureImagesMap = new Dictionary<long, List<OsmStructureImage>>();

            if (request.ShouldRetrieveRating)
            {
                ratingMap = await _ratingRepository.GetRatingsRange(structureIds);
            }

            if (request.ShouldRetrieveReviews)
            {
                reviewMap = await _reviewRepository.GetReviewsForStructuresRange(structureIds);
            }

            if (request.ShouldGetImages)
            {
                structureImagesMap = await _structureImageRepository.GetStructureRangeImages(structureIds);
            }

            if (ratingMap.Count == 0 && reviewMap.Count == 0 && structureImagesMap.Count == 0) 
            {
                return structureDto;
            }

            foreach (var element in structureDto.Elements)
            {
                if (ratingMap.TryGetValue(element.Id, out var elementRating))
                {
                    element.Rating = elementRating.Rating;
                }

                if (reviewMap.TryGetValue(element.Id, out var elementReviews))
                {
                    element.Reviews = _mapper.Map<List<ReviewDto>>(elementReviews);
                }

                if (structureImagesMap.TryGetValue(element.Id, out var structureImages))
                {
                    element.ImageUrls = structureImages.Select(x => x.ImageUrl).ToList();
                }
            }

            return structureDto;
        }
    }
}
