using InclusiveCity.Domain.Dto.Base;

namespace InclusiveCity.Domain.Dto.Routes
{
    public class RoutesApiResponseDto : GoogleMapsBaseResponseDto
    {
        public Route[] Routes { get; set; }
    }

    public class Route
    {
        public Leg[] Legs { get; set; }
        public int DistanceMeters { get; set; }
        public string Duration { get; set; }
        public Polyline Polyline { get; set; }
    }

    public class Leg
    {
        public int DistanceMeters { get; set; }
        public string Duration { get; set; }
        public Polyline Polyline { get; set; }
        public RouteLocation StartLocation { get; set; }
        public RouteLocation EndLocation { get; set; }
        public Step[] Steps { get; set; }
    }

    public class Step
    {
        public int DistanceMeters { get; set; }
        public string StaticDuration { get; set; }
        public Polyline Polyline { get; set; }
        public RouteLocation StartLocation { get; set; }
        public RouteLocation EndLocation { get; set; }
        public NavigationInstruction NavigationInstruction { get; set; }
    }

    public class NavigationInstruction
    {
        public string Maneuver { get; set; }
        public string Instructions { get; set; }
    }

    public class Polyline
    {
        public string EncodedPolyline { get; set; }
    }
}
