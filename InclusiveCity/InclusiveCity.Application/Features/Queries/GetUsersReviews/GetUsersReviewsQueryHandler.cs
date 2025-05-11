using AutoMapper;
using InclusiveCity.Contracts.Dto;
using InclusiveCity.Domain.Interfaces.Repositories;
using MediatR;

namespace InclusiveCity.Application.Features.Queries.GetUsersReviews
{
    public class GetUsersReviewsQueryHandler(IReviewRepository _reviewRepository, IMapper _mapper)
        : IRequestHandler<GetUsersReviewsQuery, IEnumerable<ReviewDto>>
    {
        public async Task<IEnumerable<ReviewDto>> Handle(GetUsersReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetReviewsByUserId(request.UserId);

            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }
    }
}
