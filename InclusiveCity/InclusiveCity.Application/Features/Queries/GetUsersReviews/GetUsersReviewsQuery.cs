using InclusiveCity.Contracts.Dto;
using MediatR;

namespace InclusiveCity.Application.Features.Queries.GetUsersReviews
{
    public class GetUsersReviewsQuery : IRequest<IEnumerable<ReviewDto>>
    {
        public Guid UserId { get; set; }
    }
}
