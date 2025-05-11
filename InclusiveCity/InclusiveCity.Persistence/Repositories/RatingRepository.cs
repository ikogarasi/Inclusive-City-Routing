using InclusiveCity.Domain.Entities;
using InclusiveCity.Domain.Interfaces.Repositories;
using InclusiveCity.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace InclusiveCity.Persistence.Repositories
{
    public class RatingRepository(ApplicationDbContext _dbContext) : IRatingRepository
    {
        public async Task<OsmRating> UpsertObjectRating(long osmId, double newAverageRating)
        {
            var osmObjectRatingFromDb = await _dbContext.OsmRatings
                .FirstOrDefaultAsync(i => i.OsmId == osmId);

            if (osmObjectRatingFromDb != null)
            {
                osmObjectRatingFromDb.Rating = newAverageRating;
            }
            else
            {
                osmObjectRatingFromDb = new OsmRating
                {
                    OsmId = osmId,
                    Rating = newAverageRating
                };

                _dbContext.OsmRatings.Add(osmObjectRatingFromDb);
            }

            await _dbContext.SaveChangesAsync();

            return osmObjectRatingFromDb;
        }

        public async Task<OsmRating?> GetObjectRating(long osmId)
        {
            return await _dbContext.OsmRatings.FirstOrDefaultAsync(i => i.OsmId == osmId);
        }

        public async Task<Dictionary<long, OsmRating>> GetRatingsRange(IEnumerable<long> ids)
        {
            return await _dbContext.OsmRatings
                .Where(r => ids.Contains(r.OsmId))
                .ToDictionaryAsync(r => r.OsmId);
        }
    }
}
