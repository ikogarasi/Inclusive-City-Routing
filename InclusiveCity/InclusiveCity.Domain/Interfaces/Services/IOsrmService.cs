using InclusiveCity.Contracts.Dto;

namespace InclusiveCity.Domain.Interfaces.Services
{
    public interface IOsrmService
    {
        Task<OsrmRouteDto> GetComputedRoute(RouteCoordinatesDto requestData);
    }
}
