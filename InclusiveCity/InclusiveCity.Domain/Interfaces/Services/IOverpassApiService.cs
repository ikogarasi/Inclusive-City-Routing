using InclusiveCity.Contracts.Dto;
using InclusiveCity.Domain.Enums;

namespace InclusiveCity.Domain.Interfaces.Services
{
    public interface IOverpassApiService
    {
        Task<OverpassResponseDto> GetStructures(OverpassRequestDto requestData);
        Task<OverpassElementDto?> GetStructureById(long osmId, string type);
    }
}