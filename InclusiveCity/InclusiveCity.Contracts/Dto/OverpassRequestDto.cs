namespace InclusiveCity.Contracts.Dto
{
    public class OverpassRequestDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Around { get; set; }
        public string Amenity { get; set; }
        public bool IsWheelChair { get; set; }
    }
}
