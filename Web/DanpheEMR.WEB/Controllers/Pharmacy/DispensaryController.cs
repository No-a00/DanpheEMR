using DanpheEMR.Application.Features.Pharmacy.Commands.DispenseDrugs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DanpheEMR.WEB.Controllers.Pharmacy
{
    [Route("api/dispensary")]
    public class DispensaryController : ApiControllerBase
    {
        // POST: api/dispensary/dispense
        // Xuất bán/phát thuốc cho bệnh nhân (trừ tồn kho)
        [HttpPost("dispense")]
        public async Task<IActionResult> DispenseDrugs([FromBody] DispenseDrugsCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}