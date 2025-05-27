using AutoMapper;
using InclusiveCity.Application.Dto;
using InclusiveCity.Contracts.Dto;
using InclusiveCity.Domain.Entities;
using InclusiveCity.Domain.Interfaces.Repositories;
using InclusiveCity.Domain.Interfaces.Services;
using MediatR;

namespace InclusiveCity.Application.Features.Queries.GetInclusiveInfrastructure
{
    public class GetInclusiveInfrastructureQueryHandler : IRequestHandler<GetInclusiveInfrastructureQuery, GetStructuresDto>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IStructureImageRepository _structureImageRepository;
        private readonly IOverpassApiService _overpassApiService;
        private readonly IMapper _mapper;

        public GetInclusiveInfrastructureQueryHandler(
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

        public async Task<GetStructuresDto> Handle(GetInclusiveInfrastructureQuery request, CancellationToken cancellationToken)
        {
            var requestDto = new InclusiveInfrastructureRequestDto
            {
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Around = request.Around,
                Toilets = request.Toilets,
                BusStops = request.BusStops,
                Kerbs = request.Kerbs,
                TactilePaving = request.TactilePaving,
                Ramps = request.Ramps
            };

            var overpassResponseDto = await _overpassApiService.GetInclusiveInfrastructure(requestDto);
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