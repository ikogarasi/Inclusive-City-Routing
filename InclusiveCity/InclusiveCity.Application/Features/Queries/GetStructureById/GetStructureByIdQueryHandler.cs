using AutoMapper;
using InclusiveCity.Application.Dto;
using InclusiveCity.Contracts.Dto;
using InclusiveCity.Domain.Interfaces.Repositories;
using InclusiveCity.Domain.Interfaces.Services;
using MediatR;

namespace InclusiveCity.Application.Features.Queries.GetStructureById
{
    public class GetStructureByIdQueryHandler : IRequestHandler<GetStructureByIdQuery, ElementDto?>
    {
        private readonly IOverpassApiService _overpassApiService;
        private readonly IReviewRepository _reviewRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IStructureImageRepository _structureImageRepository;
        private readonly IMapper _mapper;

        public GetStructureByIdQueryHandler(IOverpassApiService overpassApiService, 
            IReviewRepository reviewRepository, 
            IRatingRepository ratingRepository, 
            IStructureImageRepository structureImageRepository, 
            IMapper mapper)
        {
            _overpassApiService = overpassApiService;
            _reviewRepository = reviewRepository;
            _ratingRepository = ratingRepository;
            _structureImageRepository = structureImageRepository;
            _mapper = mapper;
        }

        public async Task<ElementDto?> Handle(GetStructureByIdQuery request, CancellationToken cancellationToken)
        {
            var overpassResponseDto = await _overpassApiService.GetStructureById(request.OsmId, request.Type);

            if (overpassResponseDto == null) 
            {
                return null;
            }

            var structureDto = _mapper.Map<ElementDto>(overpassResponseDto);

            if (request.ShouldRetrieveRating)
            {
                var ratingDto = await _ratingRepository.GetObjectRating(overpassResponseDto.Id);
                
                structureDto.Rating = ratingDto != null ? ratingDto.Rating : 0;
            }

            if (request.ShouldRetrieveReviews)
            {
                var reviews = await _reviewRepository.GetReviewsByObjectId(overpassResponseDto.Id);

                structureDto.Reviews = _mapper.Map<List<ReviewDto>>(reviews);
            }

            if (request.ShouldGetImages)
            {
                var imageUrls = await _structureImageRepository.GetStructureImages(overpassResponseDto.Id);
            
                structureDto.ImageUrls = [.. imageUrls.Select(i => i.ImageUrl)];
            }

            return structureDto;
        }
    }
}
