namespace InclusiveCity.Contracts.Dto
{
    public class OsrmRouteDto
    {
        public string Code { get; set; }
        public List<Route> Routes { get; set; }
        public List<Waypoint> Waypoints { get; set; }
    }

    public class Route
    {
        public string WeightName { get; set; }
        public double Weight { get; set; }
        public double Duration { get; set; }
        public double Distance { get; set; }
        public string Geometry { get; set; }
        public List<Leg> Legs { get; set; }
    }

    public class Leg
    {
        public double Duration { get; set; }
        public double Distance { get; set; }
        public double Weight { get; set; }
        public string Summary { get; set; }
        public List<Step> Steps { get; set; }
        public Annotation Annotation { get; set; }
    }

    public class Step
    {
        public double Duration { get; set; }
        public double Distance { get; set; }
        public double Weight { get; set; }
        public Maneuver Maneuver { get; set; }
        public string Geometry { get; set; }
        public string Name { get; set; }
        public string Mode { get; set; }
        public string Instruction { get; set; }
    }

    public class Maneuver
    {
        public int BearingBefore { get; set; }
        public int BearingAfter { get; set; }
        public List<double> Location { get; set; }
        public string Type { get; set; }
        public string Modifier { get; set; }
        public string Exit { get; set; }
    }

    public class Annotation
    {
        public List<double> Distance { get; set; }
        public List<double> Duration { get; set; }
        public List<double> Speed { get; set; }
    }

    public class Waypoint
    {
        public string Hint { get; set; }
        public double Distance { get; set; }
        public string Name { get; set; }
        public List<double> Location { get; set; }  // [lon, lat]
    }
}
