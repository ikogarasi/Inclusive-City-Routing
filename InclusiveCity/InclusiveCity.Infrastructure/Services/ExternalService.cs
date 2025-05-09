using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace InclusiveCity.Infrastructure.Services
{
    public abstract class ExternalService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        protected readonly IConfiguration _configuration;

        public ExternalService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        protected async Task<TResponse?> SendAsync<TResponse>(HttpRequestMessage request)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Clear();

                HttpResponseMessage apiResponse = await client.SendAsync(request);

                string apiResponseContent = await apiResponse.Content.ReadAsStringAsync();
                TResponse apiResponseDto = JsonConvert.DeserializeObject<TResponse>(apiResponseContent);

                if (!apiResponse.IsSuccessStatusCode)
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
    }
}
