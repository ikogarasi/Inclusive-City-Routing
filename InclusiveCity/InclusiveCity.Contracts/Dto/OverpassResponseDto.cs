namespace InclusiveCity.Contracts.Dto
{
    public class OverpassResponseDto
    {
        public List<OverpassElementDto> Elements { get; set; } = new();
    }

    public class OverpassElementDto
    {
        public long Id { get; set; }
        public string Type { get; set; } // "node", "way", "relation"
        public double? Lat { get; set; } // For nodes
        public double? Lon { get; set; } // For nodes
        public List<long>? Nodes { get; set; } // For ways
        public Dictionary<string, string>? Tags { get; set; }
        public List<OverpassMemberDto>? Members { get; set; } // For relations
    }

    public class OverpassMemberDto
    {
        public string Type { get; set; } // "node", "way", "relation"
        public long Ref { get; set; }
        public string Role { get; set; }
    }
}
