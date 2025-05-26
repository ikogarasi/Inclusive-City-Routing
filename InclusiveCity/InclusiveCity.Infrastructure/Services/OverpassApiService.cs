using InclusiveCity.Contracts.Dto;
using InclusiveCity.Domain.Entities;
using InclusiveCity.Domain.Enums;
using InclusiveCity.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using System.Text;

namespace InclusiveCity.Infrastructure.Services
{
    public class OverpassApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        : ExternalService(httpClientFactory, configuration), IOverpassApiService
    {
        public async Task<OverpassResponseDto> GetStructures(OverpassRequestDto requestData)
        {
            var httpRequestMessage = new HttpRequestMessage();

            var url = _configuration.GetValue<string>("OverpassApi");

            var processedUrl = BuildOverpassQuery(url, requestData);

            if (!Uri.TryCreate(processedUrl, UriKind.Absolute, out var uri))
            {
                throw new InvalidDataException();
            }

            httpRequestMessage.RequestUri = uri;
            httpRequestMessage.Method = HttpMethod.Get;

            return await SendAsync<OverpassResponseDto>(httpRequestMessage);
        }

        public async Task<OverpassElementDto?> GetStructureById(long osmId, string type)
        {
            var httpRequestMessage = new HttpRequestMessage();

            var url = _configuration.GetValue<string>("OverpassApi");

            url += $"[out:json];{type.ToLower()}({osmId});out body;>;";

            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                throw new InvalidDataException();
            }

            httpRequestMessage.RequestUri = uri;
            httpRequestMessage.Method = HttpMethod.Get;

            var response = await SendAsync<OverpassResponseDto>(httpRequestMessage);

            if (response.Elements.Count > 0)
            {
                return response.Elements[0];
            }

            return null;
        }

        private string BuildOverpassQuery(string baseUrl, OverpassRequestDto requestDto)
        {
            string conditions = BuildConditions(requestDto.Name, requestDto.Amenity, requestDto.IsWheelChair);

            string nodeFilter = $"node{conditions}(around:{requestDto.Around},{requestDto.Latitude},{requestDto.Longitude});";
            string wayFilter = $"way{conditions}(around:{requestDto.Around},{requestDto.Latitude},{requestDto.Longitude});";
            string relationFilter = $"relation{conditions}(around:{requestDto.Around},{requestDto.Latitude},{requestDto.Longitude});";

            string query = $"[out:json];({nodeFilter}{wayFilter}{relationFilter});out center;";

            return baseUrl + query;
        }

        private string BuildConditions(string name = null, string amenity = null, bool isWheelChair = false)
        {
            var conditions = new List<string>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                conditions.Add($"[\"name\"~\"{name}\", i]");
            }

            if (!string.IsNullOrWhiteSpace(amenity))
            {
                conditions.Add($"[\"amenity\"=\"{amenity.ToLower()}\"]");
            }

            if (isWheelChair)
            {
                conditions.Add($"[\"wheelchair\"=\"yes\"]");
            }

            return string.Join("", conditions);
        }
    }
}
