using InclusiveCity.Contracts.Dto;
using InclusiveCity.Domain.Interfaces.Services;
using MediatR;

namespace InclusiveCity.Application.Features.Queries.ComputeInclusiveRoute
{
    public class ComputeInclusiveRouteQueryHandler(IOsrmService _osrmService) : IRequestHandler<ComputeInclusiveRouteQuery, OsrmRouteDto>
    {
        public async Task<OsrmRouteDto> Handle(ComputeInclusiveRouteQuery request, CancellationToken cancellationToken)
        {
            var osrmRequest = new RouteCoordinatesDto
            {
                OriginLongitude = request.OriginLongitude.ToString(),
                OriginLatitude = request.OriginLatitude.ToString(),
                DestinationLongitude = request.DestinationLongitude.ToString(),
                DestinationLatitude = request.DestinationLatitude.ToString(),
            };

            var response = await _osrmService.GetComputedRoute(osrmRequest);
            
            return response;
        }
    }
}
