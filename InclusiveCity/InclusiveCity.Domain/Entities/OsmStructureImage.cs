using System.ComponentModel.DataAnnotations;

namespace InclusiveCity.Domain.Entities
{
    public class OsmStructureImage
    {
        [Key]
        public int Id { get; set; }
        public long OsmId { get; set; }
        public string ImageUrl { get; set; }
    }
}
