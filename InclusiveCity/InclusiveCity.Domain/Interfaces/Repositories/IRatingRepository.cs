using InclusiveCity.Domain.Entities;

namespace InclusiveCity.Domain.Interfaces.Repositories
{
    public interface IRatingRepository
    {
        Task<OsmRating?> GetObjectRating(long osmId);
        Task<OsmRating> UpsertObjectRating(long osmId, double newAverageRating);
        Task<Dictionary<long, OsmRating>> GetRatingsRange(IEnumerable<long> ids);
    }
}