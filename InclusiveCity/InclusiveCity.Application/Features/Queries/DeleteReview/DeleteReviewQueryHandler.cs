using InclusiveCity.Domain.Repositories;
using MediatR;

namespace InclusiveCity.Application.Features.Queries.DeleteReview
{
    public class DeleteReviewQueryHandler(IReviewRepository _reviewRepository) : IRequestHandler<DeleteReviewQuery>
    {
        public async Task Handle(DeleteReviewQuery request, CancellationToken cancellationToken)
        {
            await _reviewRepository.DeleteReview(request.ReviewId);
        }
    }
}
