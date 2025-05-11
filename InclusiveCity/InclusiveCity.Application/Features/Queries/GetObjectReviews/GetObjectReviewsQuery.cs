using InclusiveCity.Contracts.Dto;
using MediatR;

namespace InclusiveCity.Application.Features.Queries.GetObjectReviews
{
    public class GetObjectReviewsQuery : IRequest<IEnumerable<ReviewDto>>
    {
        public long OsmId { get; set; }
    }
}
