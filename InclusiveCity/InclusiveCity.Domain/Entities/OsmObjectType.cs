
using System.ComponentModel.DataAnnotations;

namespace InclusiveCity.Domain.Entities
{
    public class OsmObjectType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
