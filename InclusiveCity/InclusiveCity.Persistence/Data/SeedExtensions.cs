using InclusiveCity.Domain.Entities;
using InclusiveCity.Domain.Enums;
using InclusiveCity.Domain.Extensions;
using Microsoft.EntityFrameworkCore;

namespace InclusiveCity.Persistence.Data
{
    internal static class SeedExtensions
    {
        public static void Initialize(this ModelBuilder modelBuilder)
        {
            var osmObjectTypes = Enum.GetValues(typeof(OsmType))
                .Cast<OsmType>()
                .Select(i => new OsmObjectType
                {
                    Id = (int)i,
                    Name = i.ToString(),
                    Description = i.GetDescription()
                });

            modelBuilder.Entity<OsmObjectType>().HasData(osmObjectTypes);
        }
    }
}
