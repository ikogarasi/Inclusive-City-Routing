using MediatR;

namespace InclusiveCity.Application.Features.Queries.DeleteReview
{
    public class DeleteReviewQuery : IRequest
    {
        public int ReviewId { get; set; }
    }
}
