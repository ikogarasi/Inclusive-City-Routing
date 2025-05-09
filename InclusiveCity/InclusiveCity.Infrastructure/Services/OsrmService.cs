using InclusiveCity.Contracts.Dto;
using InclusiveCity.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace InclusiveCity.Infrastructure.Services
{
    public class OsrmService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        : ExternalService(httpClientFactory, configuration), IOsrmService
    {
        public async Task<OsrmRouteDto> GetComputedRoute(RouteCoordinatesDto requestData)
        {
            var httpRequestMessage = new HttpRequestMessage();

            var url = _configuration.GetValue<string>("OsrmApi:GetComputedRouteUrl");

            var processedUrl = url.Replace("<:lon1>", requestData.OriginLongitude)
                .Replace("<:lat1>", requestData.OriginLatitude)
                .Replace("<:lon2>", requestData.DestinationLongitude)
                .Replace("<:lat2>", requestData.DestinationLatitude);

            if (!Uri.TryCreate(processedUrl, UriKind.Absolute, out var uri))
            {
                throw new InvalidDataException();
            }

            httpRequestMessage.RequestUri = uri;
            httpRequestMessage.Method = HttpMethod.Get;

            return await SendAsync<OsrmRouteDto>(httpRequestMessage);
        }
    }
}
