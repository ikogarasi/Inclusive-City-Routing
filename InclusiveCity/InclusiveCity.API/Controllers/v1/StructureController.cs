using Asp.Versioning;
using InclusiveCity.Application.Features.Queries.GetStructures;
using Microsoft.AspNetCore.Mvc;

namespace InclusiveCity.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class StructureController : ControllerApiBase
    {
        [HttpGet]
        public async Task GetStructures([FromQuery] GetStructuresQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
