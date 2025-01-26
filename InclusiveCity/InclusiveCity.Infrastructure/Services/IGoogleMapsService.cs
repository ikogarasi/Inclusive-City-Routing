using InclusiveCity.Domain.Dto.Base;

namespace InclusiveCity.Infrastructure.Services
{
    public interface IGoogleMapsService<TRequestData, TResponse> where TResponse : GoogleMapsBaseResponseDto
    {
        Task<TResponse?> SendAsync(TRequestData requestData);
    }
}