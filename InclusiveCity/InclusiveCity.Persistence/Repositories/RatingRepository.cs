using InclusiveCity.Domain.Entities;
using InclusiveCity.Domain.Repositories;
using InclusiveCity.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace InclusiveCity.Persistence.Repositories
{
    public class RatingRepository(ApplicationDbContext _dbContext) : IRatingRepository
    {
        public async Task<OsmRating> UpsertObjectRating(int osmId, double newAverageRating)
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

        public async Task<OsmRating?> GetObjectRating(int osmId)
        {
            return await _dbContext.OsmRatings.FirstOrDefaultAsync(i => i.OsmId == osmId);
        }
    }
}
