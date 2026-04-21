using DanpheEMR.Application.Features.Pharmacy.Commands.DispenseDrugs;
using DanpheEMR.WEB.Security; 
using Microsoft.AspNetCore.Mvc;


namespace DanpheEMR.WEB.Controllers.Pharmacy
{
    [Route("api/dispensary")]
    public class DispensaryController : ApiControllerBase
    {
        // POST: api/dispensary/dispense
        // Xuất bán/phát thuốc cho bệnh nhân (trừ tồn kho)
        [HttpPost("dispense")]
        [RequirePermission("Pharmacy", "Write")] 
        public async Task<IActionResult> DispenseDrugs([FromBody] DispenseDrugsCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}