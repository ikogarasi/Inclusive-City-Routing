namespace InclusiveCity.Contracts.Dto
{
    public class InclusiveInfrastructureRequestDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Around { get; set; }
        public bool Toilets { get; set; }
        public bool BusStops { get; set; }
        public bool Kerbs { get; set; }
        public bool TactilePaving { get; set; }
        public bool Ramps { get; set; }
    }
}