using Asp.Versioning;
using InclusiveCity.Application.Features.Queries.ComputeInclusiveRoute;
using Microsoft.AspNetCore.Mvc;

namespace InclusiveCity.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class RoutingController : ControllerApiBase
    {
        [HttpGet]
        public async Task<ActionResult> GetComputedRoute([FromQuery] ComputeInclusiveRouteQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
