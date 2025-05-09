using AutoMapper;
using InclusiveCity.Contracts.Dto;
using InclusiveCity.Domain.Entities;
using InclusiveCity.Domain.Repositories;
using MediatR;

namespace InclusiveCity.Application.Features.Commands.AddReview
{
    public class AddReviewCommandHandler : IRequestHandler<AddReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public AddReviewCommandHandler(
            IReviewRepository reviewRepository,
            IRatingRepository ratingRepository,
            IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewEntity = _mapper.Map<OsmReview>(request);
            await _reviewRepository.AddReview(reviewEntity);

            double averageRate = await _reviewRepository.GetAverageRateFromReviews(request.OsmId);
            await _ratingRepository.UpsertObjectRating(request.OsmId, averageRate);
        }
    }
}
