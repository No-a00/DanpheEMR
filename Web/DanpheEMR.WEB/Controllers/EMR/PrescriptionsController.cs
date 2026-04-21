using DanpheEMR.Application.Features.EMR.Commands.CreatePrescription;
using DanpheEMR.WEB.Security; 
using Microsoft.AspNetCore.Mvc;

namespace DanpheEMR.WEB.Controllers.Clinical
{
    [Route("api/prescriptions")]
    public class PrescriptionsController : ApiControllerBase
    {
        // POST: api/prescriptions
        [HttpPost]
        [RequirePermission("EMR", "Full")] 
        public async Task<IActionResult> CreatePrescription([FromBody] CreatePrescriptionCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}