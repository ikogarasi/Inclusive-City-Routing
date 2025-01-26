using InclusiveCity.Domain.Dto.Routes;
using Microsoft.Extensions.Configuration;

namespace InclusiveCity.Infrastructure.Services
{
    public class RoutesService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        : GoogleMapsBaseService<RoutesApiRequestDto, RoutesApiResponseDto>(httpClientFactory, configuration)
    {
        protected override HttpRequestMessage CreateBaseHttpRequest()
        {
            var httpRequestMessage = new HttpRequestMessage();

            var urlData = _configuration.GetValue<string>("GoogleMapsAPI:RouteAPIUrl");

            if (!Uri.TryCreate(urlData, UriKind.Absolute, out var uri))
            {
                throw new InvalidDataException();
            }

            httpRequestMessage.RequestUri = uri;
            httpRequestMessage.Method = HttpMethod.Post;
            httpRequestMessage.Headers.Add("X-Goog-FieldMask", 
                "routes.duration,routes.distanceMeters,routes.polyline,routes.legs.distanceMeters,routes.legs.duration,routes.legs.polyline,routes.legs.startLocation,routes.legs.endLocation,routes.legs.steps.distanceMeters,routes.legs.steps.staticDuration,routes.legs.steps.polyline,routes.legs.steps.startLocation,routes.legs.steps.endLocation,routes.legs.steps.navigationInstruction");

            return httpRequestMessage;
        }
    }
}
