using Asp.Versioning;
using InclusiveCity.Application.Features.Queries.ComputeInclusiveRoute;
using InclusiveCity.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InclusiveCity.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class RoutingController : ControllerApiBase
    {
        [HttpGet]
        public async Task<ActionResult<OsrmRouteDto>> GetComputedRoute([FromQuery] ComputeInclusiveRouteQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
