using InclusiveCity.Contracts.Dto;

namespace InclusiveCity.Domain.Interfaces.Services
{
    public interface IOverpassApiService
    {
        Task<OverpassResponseDto> GetStructures(OverpassRequestDto requestData);
    }
}