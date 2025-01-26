using InclusiveCity.Application.Dto;
using MediatR;

namespace InclusiveCity.Application.Features.ComputeInclusiveRoute.Queries
{
    public class ComputeInclusiveRouteQuery : IRequest<ComputedInclusiveRouteDto>
    {
        public double OriginLatitude { get; set; }
        public double OriginLongitude { get; set; }
        public double DestinationLatitude { get; set; }
        public double DestinationLongitude { get; set; }
    }
}
