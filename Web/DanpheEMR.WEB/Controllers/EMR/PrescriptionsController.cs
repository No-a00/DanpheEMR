
using DanpheEMR.Application.Features.EMR.Commands.CreatePrescription;
using Microsoft.AspNetCore.Mvc;

namespace DanpheEMR.WEB.Controllers.Clinical
{
    [Route("api/prescriptions")]
    public class PrescriptionsController : ApiControllerBase
    {
        // POST: api/prescriptions
        [HttpPost]
        public async Task<IActionResult> CreatePrescription([FromBody] CreatePrescriptionCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}