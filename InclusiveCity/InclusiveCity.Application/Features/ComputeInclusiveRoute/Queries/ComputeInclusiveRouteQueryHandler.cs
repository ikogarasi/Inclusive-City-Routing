using InclusiveCity.Application.Dto;
using InclusiveCity.Domain.Dto.Routes;
using InclusiveCity.Infrastructure.Services;
using MediatR;
using Newtonsoft.Json;

namespace InclusiveCity.Application.Features.ComputeInclusiveRoute.Queries
{
    public class ComputeInclusiveRouteQueryHandler(IGoogleMapsService<RoutesApiRequestDto, RoutesApiResponseDto> _routesService) 
        : IRequestHandler<ComputeInclusiveRouteQuery, ComputedInclusiveRouteDto>
    {
        public async Task<ComputedInclusiveRouteDto> Handle(ComputeInclusiveRouteQuery request, CancellationToken cancellationToken)
        {
            var response = await _routesService.SendAsync(new (request.OriginLatitude, request.OriginLongitude, request.DestinationLatitude, request.DestinationLongitude));

            Console.WriteLine(JsonConvert.SerializeObject(response));

            return new ComputedInclusiveRouteDto();
        }
    }
}
