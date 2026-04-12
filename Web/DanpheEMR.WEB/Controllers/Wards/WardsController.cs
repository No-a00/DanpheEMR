using DanpheEMR.Application.Features.Inpatient.Commands.SetupWard;
using DanpheEMR.Application.Features.Inpatient.Queries.GetBedsByWard;
using DanpheEMR.Application.Features.Inpatient.Queries.GetWardOccupancy;
using Microsoft.AspNetCore.Mvc;

namespace DanpheEMR.WEB.Controllers.Wards
{
    [Route("api/wards")]
    public class WardsController : ApiControllerBase
    {
        // POST: api/wards
        [HttpPost]
        public async Task<IActionResult> SetupWard([FromBody] SetupWardCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // GET: api/wards/occupancy
        [HttpGet("occupancy")]
        public async Task<IActionResult> GetWardOccupancy([FromQuery] GetWardOccupancyQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // GET: api/wards/{wardId}/beds
        [HttpGet("{wardId}/beds")]
        public async Task<IActionResult> GetBedsByWard(Guid wardId)
        {
            var result = await Mediator.Send(new GetBedsByWardQuery(wardId));
            return Ok(result);
        }
    }
}