using InclusiveCity.Contracts.Dto;
using MediatR;

namespace InclusiveCity.Application.Features.Queries.ComputeInclusiveRoute
{
    public class ComputeInclusiveRouteQuery : IRequest<OsrmRouteDto>
    {
        public double OriginLatitude { get; set; }
        public double OriginLongitude { get; set; }
        public double DestinationLatitude { get; set; }
        public double DestinationLongitude { get; set; }
    }
}
