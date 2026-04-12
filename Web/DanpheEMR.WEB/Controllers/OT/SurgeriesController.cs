using DanpheEMR.Application.Features.OT.Commands.ScheduleSurgery;
using DanpheEMR.Application.Features.OT.Commands.UpdateSurgeryStatus;
using DanpheEMR.Application.Features.OT.Queries.GetDailySurgerySchedule;
using Microsoft.AspNetCore.Mvc;

namespace DanpheEMR.WEB.Controllers.OT
{
    [Route("api/surgeries")]
    public class SurgeriesController : ApiControllerBase
    {
        // GET: api/surgeries/daily?date=2023-10-25
        [HttpGet("daily")]
        public async Task<IActionResult> GetDailySurgerySchedule([FromQuery] GetDailySurgeryScheduleQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // POST: api/surgeries
        [HttpPost]
        public async Task<IActionResult> ScheduleSurgery([FromBody] ScheduleSurgeryCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // PUT: api/surgeries/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateSurgeryStatus(Guid id, [FromBody] UpdateSurgeryStatusCommand command)
        {
            if (id != command.ScheduleId) return BadRequest("ID ca phẫu thuật không khớp.");

            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}