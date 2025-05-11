using InclusiveCity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InclusiveCity.Persistence.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : DbContext(options)
    {
        public DbSet<OsmRating> OsmRatings { get; set; }

        public DbSet<OsmReview> OsmReviews { get; set; }

        public DbSet<OsmStructureImage> OsmStructureImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var migrations = Database.GetAppliedMigrations();

            if (!migrations.Contains("InitDatabase"))
            {
                SeedExtensions.Initialize(modelBuilder);
            }
        }
    }
}
