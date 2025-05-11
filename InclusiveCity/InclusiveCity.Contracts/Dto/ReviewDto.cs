namespace InclusiveCity.Contracts.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public long OsmId { get; set; }

        public string OsmType { get; set; }

        public string Comment { get; set; }

        public string? PhotoUrl { get; set; }

        public Guid CreatedBy { get; set; }

        public string Username { get; set; }

        public DateTime CreatedAt { get; set; }

        public double Rate { get; set; }
    }
}
