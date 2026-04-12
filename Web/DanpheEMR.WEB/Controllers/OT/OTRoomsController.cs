using DanpheEMR.Application.Features.OT.Commands.SetupOTRoom;
using DanpheEMR.Application.Features.OT.Queries.GetAvailableOTRooms;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DanpheEMR.WEB.Controllers.OT
{
    [Route("api/ot-rooms")]
    public class OTRoomsController : ApiControllerBase
    {
        // GET: api/ot-rooms/available
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableOTRooms([FromQuery] GetAvailableOTRoomsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // POST: api/ot-rooms
        [HttpPost]
        public async Task<IActionResult> SetupOTRoom([FromBody] SetupOTRoomCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}