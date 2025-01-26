namespace InclusiveCity.Domain.Dto.Routes
{
    public class RoutesApiRequestDto
    {
        public RoutesApiRequestDto(double originLatitude, 
            double originLongitude, 
            double destinationLatitude, 
            double destinationLongitude)
        {
            Origin = new()
            {
                Location = new()
                {
                    LatLng = new()
                    {
                        Latitude = originLatitude,
                        Longitude = originLongitude
                    }
                }
            };

            Destination = new()
            {
                Location = new()
                {
                    LatLng = new()
                    {
                        Latitude = destinationLatitude,
                        Longitude = destinationLongitude
                    }
                }
            };
        }

        public RouteWaypoint Origin { get; set; }
        public RouteWaypoint Destination { get; set; }
        public string TravelMode => "Walk";
        public bool ComputeAlternativeRoutes => true;
    }

    public class RouteWaypoint
    {
        public RouteLocation Location { get; set; }
    }

    public class RouteLocation
    {
        public Coordinates LatLng { get; set; }
    }

    public class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
