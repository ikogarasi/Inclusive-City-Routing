using InclusiveCity.Application.Dto;
using MediatR;

namespace InclusiveCity.Application.Features.Queries.GetStructures
{
    public class GetStructuresQuery : IRequest<GetStructuresDto>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Around { get; set; }
        public string Amenity { get; set; }
        public bool IsWheelChair { get; set; }
        public bool ShouldRetrieveRating { get; set; }
        public bool ShouldRetrieveReviews { get; set; }
        public bool ShouldGetImages { get; set; }
    }
}