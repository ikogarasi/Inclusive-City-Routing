using InclusiveCity.Domain.Entities;
using InclusiveCity.Domain.Interfaces.Repositories;
using InclusiveCity.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace InclusiveCity.Persistence.Repositories
{
    public class ReviewRepository(ApplicationDbContext _dbContext) : IReviewRepository
    {
        public async Task<OsmReview?> GetReviewById(int reviewId)
        {
            return await _dbContext.OsmReviews.FirstOrDefaultAsync(i => i.Id == reviewId);
        }

        public async Task<IEnumerable<OsmReview>> GetReviewsByUserId(Guid userId)
        {
            return await _dbContext.OsmReviews.Where(i => i.CreatedBy == userId).ToListAsync();
        }

        public async Task<IEnumerable<OsmReview>> GetReviewsByObjectId(long osmId)
        {
            return await _dbContext.OsmReviews.Where(i => i.OsmId == osmId).ToListAsync();
        }

        public async Task<double> GetAverageRateFromReviews(long osmId)
        {
            return await _dbContext.OsmReviews
                .Where(i => i.OsmId == osmId)
                .Select(i => i.Rate)
                .AverageAsync();
        }

        public async Task<Dictionary<long, List<OsmReview>>> GetReviewsForStructuresRange(IEnumerable<long> ids)
        {
            return await _dbContext.OsmReviews
                .Where(i => ids.Contains(i.OsmId))
                .GroupBy(r => r.OsmId)
                .ToDictionaryAsync(g => g.Key, g => g.ToList());
        }

        public async Task AddReview(OsmReview osmReview)
        {
            ArgumentNullException.ThrowIfNull(osmReview);

            await _dbContext.AddAsync(osmReview);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteReview(int reviewId)
        {
            var entity = await GetReviewById(reviewId);

            ArgumentNullException.ThrowIfNull(entity);

            _dbContext.OsmReviews.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
