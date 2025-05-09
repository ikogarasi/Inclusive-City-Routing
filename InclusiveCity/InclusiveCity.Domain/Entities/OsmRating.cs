using System.ComponentModel.DataAnnotations;

namespace InclusiveCity.Domain.Entities
{
    public class OsmRating
    {
        [Key]
        public int Id { get; set; }
    
        public int OsmId { get; set; }

        public double Rating { get; set; }
    }
}
