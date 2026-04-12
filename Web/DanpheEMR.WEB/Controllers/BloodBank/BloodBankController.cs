using DanpheEMR.Application.Features.BloodBank.Commands.IssueBlood;
using DanpheEMR.Application.Features.BloodBank.Commands.RecordDonation;
using DanpheEMR.Application.Features.BloodBank.Commands.RegisterDonor;
using DanpheEMR.Application.Features.BloodBank.Queries.GetBloodInventory;
using DanpheEMR.Application.Features.BloodBank.Queries.GetEligibleDonors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DanpheEMR.WEB.Controllers.BloodBank
{
    [Route("api/blood-bank")]
    public class BloodBankController : ApiControllerBase
    {
        // POST: api/blood-bank/donors
        [HttpPost("donors")]
        public async Task<IActionResult> RegisterDonor([FromBody] RegisterDonorCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // POST: api/blood-bank/donations
        [HttpPost("donations")]
        public async Task<IActionResult> RecordDonation([FromBody] RecordDonationCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // POST: api/blood-bank/issues
        [HttpPost("issues")]
        public async Task<IActionResult> IssueBlood([FromBody] IssueBloodCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // GET: api/blood-bank/inventory?bloodGroup=O+
        [HttpGet("inventory")]
        public async Task<IActionResult> GetBloodInventory([FromQuery] GetBloodInventoryQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // GET: api/blood-bank/donors/eligible?bloodGroup=A+
        [HttpGet("donors/eligible")]
        public async Task<IActionResult> GetEligibleDonors([FromQuery] GetEligibleDonorsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}