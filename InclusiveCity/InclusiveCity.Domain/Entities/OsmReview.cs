using InclusiveCity.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace InclusiveCity.Domain.Entities
{
    public class OsmReview
    {
        [Key]
        public int Id { get; set; }
    
        public long OsmId { get; set; }

        public int OsmTypeId { get; set; }

        public string Comment { get; set; }

        public string? PhotoUrl { get; set; }
        
        public string Username { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Range(0, 5)]
        public double Rate { get; set; }
    }
}
