using InclusiveCity.Application.Dto;
using MediatR;

namespace InclusiveCity.Application.Features.Queries.GetInclusiveInfrastructure
{
    public class GetInclusiveInfrastructureQuery : IRequest<GetStructuresDto>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Around { get; set; } = 300;
        public bool Toilets { get; set; }
        public bool BusStops { get; set; }
        public bool Kerbs { get; set; }
        public bool TactilePaving { get; set; }
        public bool Ramps { get; set; }
        public bool ShouldRetrieveRating { get; set; }
        public bool ShouldRetrieveReviews { get; set; }
        public bool ShouldGetImages { get; set; }
    }
}