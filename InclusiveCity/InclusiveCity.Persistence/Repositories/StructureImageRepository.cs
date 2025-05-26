using InclusiveCity.Domain.Entities;
using InclusiveCity.Domain.Interfaces.Repositories;
using InclusiveCity.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace InclusiveCity.Persistence.Repositories
{
    public class StructureImageRepository(ApplicationDbContext _dbContext) : IStructureImageRepository
    {
        public async Task AddStructureImage(long osmId, IEnumerable<string> imageUrls)
        {
            var entities = new List<OsmStructureImage>();

            foreach (var imageUrl in imageUrls)
            {
                entities.Add(new()
                {
                    OsmId = osmId,
                    ImageUrl = imageUrl
                });
            }

            await _dbContext.OsmStructureImages.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<OsmStructureImage>> GetStructureImages(long osmId)
        {
            return await _dbContext.OsmStructureImages.Where(i => i.OsmId == osmId).ToListAsync();
        }

        public async Task<Dictionary<long, List<OsmStructureImage>>> GetStructureRangeImages(IEnumerable<long> ids)
        {
            return await _dbContext.OsmStructureImages
                .Where(i => ids.Contains(i.OsmId))
                .GroupBy(r => r.OsmId)
                .ToDictionaryAsync(g => g.Key, g => g.ToList());
        }
    }
}
