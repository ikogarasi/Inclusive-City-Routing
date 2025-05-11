using InclusiveCity.Domain.Entities;

namespace InclusiveCity.Domain.Interfaces.Repositories
{
    public interface IStructureImageRepository
    {
        Task AddStructureImage(long osmId, IEnumerable<string> imageUrls);
        Task<IEnumerable<OsmStructureImage>> GetStructureImages(int osmId);
        Task<Dictionary<long, List<OsmStructureImage>>> GetStructureRangeImages(IEnumerable<long> ids);
    }
}