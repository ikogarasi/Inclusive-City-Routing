using AutoMapper;
using InclusiveCity.Azure.BlobStorage;
using InclusiveCity.Azure.BlobStorage.Dto;
using InclusiveCity.Azure.BlobStorage.Enums;
using InclusiveCity.Domain.Entities;
using InclusiveCity.Domain.Interfaces.Repositories;
using MediatR;

namespace InclusiveCity.Application.Features.Commands.AddReview
{
    public class AddReviewCommandHandler : IRequestHandler<AddReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IAzureStorage _azureStorage;
        private readonly IMapper _mapper;

        public AddReviewCommandHandler(
            IReviewRepository reviewRepository,
            IRatingRepository ratingRepository,
            IAzureStorage azureStorage,
            IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _reviewRepository = reviewRepository;
            _azureStorage = azureStorage;
            _mapper = mapper;
        }

        public async Task Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewEntity = _mapper.Map<OsmReview>(request);

            if (!string.IsNullOrEmpty(request.ImageBase64))
            {
                byte[] byteImage = Convert.FromBase64String(request.ImageBase64);
                BlobResponseDto response = await _azureStorage.UploadAsync(byteImage, $"review-{request.OsmId}", ContainerType.Review);

                if (response.Error || response.Blob == null)
                {
                    throw new InvalidOperationException(response.Status);
                }

                reviewEntity.PhotoUrl = response.Blob.Uri;
            }

            await _reviewRepository.AddReview(reviewEntity);

            double averageRate = await _reviewRepository.GetAverageRateFromReviews(request.OsmId);
            await _ratingRepository.UpsertObjectRating(request.OsmId, averageRate);
        }
    }
}
