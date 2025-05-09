using InclusiveCity.Domain.Entities;

namespace InclusiveCity.Domain.Repositories
{
    public interface IRatingRepository
    {
        Task<OsmRating?> GetObjectRating(int osmId);
        Task<OsmRating> UpsertObjectRating(int osmId, double newAverageRating);
    }
}