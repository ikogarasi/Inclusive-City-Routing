using InclusiveCity.Application.Dto;
using MediatR;

namespace InclusiveCity.Application.Features.Queries.GetStructureById
{
    public class GetStructureByIdQuery : IRequest<ElementDto?>
    {
        public long OsmId { get; set; }
        public string Type { get; set; }
        public bool ShouldRetrieveRating { get; set; }
        public bool ShouldRetrieveReviews { get; set; }
        public bool ShouldGetImages { get; set; }
    }
}
