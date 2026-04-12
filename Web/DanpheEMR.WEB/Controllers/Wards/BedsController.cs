using DanpheEMR.Application.Features.Inpatient.Commands.AddBed;
using DanpheEMR.Application.Features.Inpatient.Commands.UpdateBedStatus;
using DanpheEMR.Application.Features.Inpatient.Queries.GetAvailableBeds;
using Microsoft.AspNetCore.Mvc;


namespace DanpheEMR.WEB.Controllers.Wards
{
    [Route("api/beds")]
    public class BedsController : ApiControllerBase
    {
        // GET: api/beds/available
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableBeds([FromQuery] GetAvailableBedsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // POST: api/beds
        [HttpPost]
        public async Task<IActionResult> AddBed([FromBody] AddBedCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // PUT: api/beds/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateBedStatus(Guid id, [FromBody] UpdateBedStatusCommand command)
        {
            if (id != command.BedId)
            {
                return BadRequest("ID giường bệnh không khớp.");
            }

            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}