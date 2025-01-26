using Asp.Versioning;
using InclusiveCity.Application.Dto;
using InclusiveCity.Application.Features.ComputeInclusiveRoute.Queries;
using Microsoft.AspNetCore.Mvc;

namespace InclusiveCity.API.Controllers.v1
{
    public class InclusiveRoutesController : ControllerApiBase
    {
        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<ComputedInclusiveRouteDto>> ComputeInclusiveRoute([FromQuery] ComputeInclusiveRouteQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
