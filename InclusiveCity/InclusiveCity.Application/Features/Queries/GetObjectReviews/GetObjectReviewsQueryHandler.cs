using AutoMapper;
using InclusiveCity.Contracts.Dto;
using InclusiveCity.Domain.Repositories;
using MediatR;

namespace InclusiveCity.Application.Features.Queries.GetObjectReviews
{
    public class GetObjectReviewsQueryHandler(IReviewRepository _reviewRepository, IMapper _mapper)
        : IRequestHandler<GetObjectReviewsQuery, IEnumerable<ReviewDto>>
    {
        public async Task<IEnumerable<ReviewDto>> Handle(GetObjectReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetReviewsByObjectId(request.OsmId);

            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }
    }
}
