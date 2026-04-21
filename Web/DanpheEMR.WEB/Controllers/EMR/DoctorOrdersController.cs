using DanpheEMR.Application.Features.EMR.Commands.CreateDoctorOrder;
using DanpheEMR.Application.Features.EMR.Queries.GetPendingDoctorOrders;
using DanpheEMR.WEB.Security; 
using Microsoft.AspNetCore.Mvc;

namespace DanpheEMR.WEB.Controllers.Clinical
{
    [Route("api/doctor-orders")]
    public class DoctorOrdersController : ApiControllerBase
    {
        // GET: api/doctor-orders/pending
        [HttpGet("pending")]
        [RequirePermission("EMR", "Read")] 
        public async Task<IActionResult> GetPendingDoctorOrders([FromQuery] GetPendingDoctorOrdersQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // POST: api/doctor-orders
        [HttpPost]
        [RequirePermission("EMR", "Write")] 
        public async Task<IActionResult> CreateDoctorOrder([FromBody] CreateDoctorOrderCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}