using InclusiveCity.Contracts.Dto;
using InclusiveCity.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace InclusiveCity.Infrastructure.Services
{
    public class OverpassApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        : ExternalService(httpClientFactory, configuration), IOverpassApiService
    {
        public async Task<OverpassResponseDto> GetStructures(OverpassRequestDto requestData)
        {
            var httpRequestMessage = new HttpRequestMessage();

            var url = _configuration.GetValue<string>("OverpassApi:GetObjects");

            var processedUrl = url.Replace("<:amenity>", requestData.Amenity)
                .Replace("<:around>", requestData.Around.ToString())
                .Replace("<:lat>", requestData.Latitude.ToString())
                .Replace("<:lon>", requestData.Longitude.ToString())
                .Replace("<:isWheelChair>", requestData.IsWheelChair ? "[\"wheelchair\"=\"yes\"]" : "");

            if (!Uri.TryCreate(processedUrl, UriKind.Absolute, out var uri))
            {
                throw new InvalidDataException();
            }

            httpRequestMessage.RequestUri = uri;
            httpRequestMessage.Method = HttpMethod.Get;

            return await SendAsync<OverpassResponseDto>(httpRequestMessage);
        }
    }
}
