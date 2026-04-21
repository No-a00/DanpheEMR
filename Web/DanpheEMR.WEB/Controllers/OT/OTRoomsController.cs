using DanpheEMR.Application.Features.OT.Commands.SetupOTRoom;
using DanpheEMR.Application.Features.OT.Queries.GetAvailableOTRooms;
using DanpheEMR.WEB.Security;
using Microsoft.AspNetCore.Mvc;


namespace DanpheEMR.WEB.Controllers.OT
{
    [Route("api/ot-rooms")]
    public class OTRoomsController : ApiControllerBase
    {
        // GET: api/ot-rooms/available
        [HttpGet("available")]
        [RequirePermission("OT", "Read")] 
        public async Task<IActionResult> GetAvailableOTRooms([FromQuery] GetAvailableOTRoomsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // POST: api/ot-rooms
        [HttpPost]
        [RequirePermission("OT", "Write")] 
        public async Task<IActionResult> SetupOTRoom([FromBody] SetupOTRoomCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}