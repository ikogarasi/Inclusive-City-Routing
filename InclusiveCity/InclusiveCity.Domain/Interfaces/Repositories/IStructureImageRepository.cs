using InclusiveCity.Domain.Entities;

namespace InclusiveCity.Domain.Interfaces.Repositories
{
    public interface IStructureImageRepository
    {
        Task AddStructureImage(long osmId, IEnumerable<string> imageUrls);
        Task<IEnumerable<OsmStructureImage>> GetStructureImages(long osmId);
        Task<Dictionary<long, List<OsmStructureImage>>> GetStructureRangeImages(IEnumerable<long> ids);
    }
}