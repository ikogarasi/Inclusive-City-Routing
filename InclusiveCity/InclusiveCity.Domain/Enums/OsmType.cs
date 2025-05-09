using System.ComponentModel;

namespace InclusiveCity.Domain.Enums
{
    public enum OsmType
    {
        [Description("A single point with latitude and longitude.")]
        Node = 1,

        [Description("An ordered list of nodes (lines, polygons).")]
        Way = 2,

        [Description("A collection of nodes, ways, and/or other relations with roles.")]
        Relation = 3
    }
}
