using InclusiveCity.Domain.Dto.Base;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace InclusiveCity.Infrastructure.Services
{
    public abstract class GoogleMapsBaseService<TRequestData, TResponse> : IGoogleMapsService<TRequestData, TResponse>
        where TResponse : GoogleMapsBaseResponseDto
    {
        private readonly IHttpClientFactory _httpClientFactory;
        protected readonly IConfiguration _configuration;

        private readonly string X_GOOGLE_API_KEY_HEADER = "X-Goog-Api-Key";

        public GoogleMapsBaseService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<TResponse?> SendAsync(TRequestData requestData)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();

                HttpRequestMessage request = CreateBaseHttpRequest();

                string apiKey = _configuration.GetValue<string>(X_GOOGLE_API_KEY_HEADER)
                    ?? throw new KeyNotFoundException();

                request.Headers.Add(X_GOOGLE_API_KEY_HEADER, apiKey);

                client.DefaultRequestHeaders.Clear();
                
                if (requestData != null)
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(requestData, GetJsonSerializerSettings()), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage apiResponse = await client.SendAsync(request);

                string apiResponseContent = await apiResponse.Content.ReadAsStringAsync();
                TResponse apiResponseDto = JsonConvert.DeserializeObject<TResponse>(apiResponseContent);

                if (apiResponseDto.Error != null)
                {
                    throw new Exception();
                }

                return apiResponseDto;
            }
            catch
            {
                // pass error and log it;

                throw;
            }
        }

        private JsonSerializerSettings GetJsonSerializerSettings()
        {
            DefaultContractResolver contractResolver = new()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            return new()
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };
        }

        protected abstract HttpRequestMessage CreateBaseHttpRequest();
    }
}
