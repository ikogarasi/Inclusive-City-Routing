using InclusiveCity.Contracts.Dto;

namespace InclusiveCity.Application.Dto
{
    public class GetStructuresDto
    {
        public List<ElementDto> Elements { get; set; }
    }

    public class ElementDto : OverpassElementDto
    {
        public double Rating { get; set; }
        public List<ReviewDto>? Reviews { get; set; }
        public List<string>? ImageUrls { get; set; }
    }
}
