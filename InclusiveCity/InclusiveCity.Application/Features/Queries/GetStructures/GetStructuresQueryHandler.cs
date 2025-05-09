using AutoMapper;
using InclusiveCity.Contracts.Dto;
using InclusiveCity.Domain.Interfaces.Services;
using InclusiveCity.Domain.Repositories;
using MediatR;

namespace InclusiveCity.Application.Features.Queries.GetStructures
{
    public class GetStructuresQueryHandler : IRequestHandler<GetStructuresQuery>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IOverpassApiService _overpassApiService;
        private readonly IMapper _mapper;

        public GetStructuresQueryHandler(
            IReviewRepository reviewRepository,
            IOverpassApiService overpassApiService, 
            IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _overpassApiService = overpassApiService;
            _mapper = mapper;
        }

        public async Task Handle(GetStructuresQuery request, CancellationToken cancellationToken)
        {
            var requestDto = _mapper.Map<OverpassRequestDto>(request);
            var overpassResponseDto = await _overpassApiService.GetStructures(requestDto);

            // add check for comments and rarjng
        }
    }
}
