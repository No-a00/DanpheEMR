using DanpheEMR.Application.Features.Pharmacy.Commands.AddSupplier;
using DanpheEMR.Application.Features.Pharmacy.Commands.SetupPharmacyItem;
using DanpheEMR.Application.Features.Pharmacy.Queries.GetPharmacyItems;
using DanpheEMR.WEB.Security; 
using Microsoft.AspNetCore.Mvc;


namespace DanpheEMR.WEB.Controllers.Pharmacy
{
    [Route("api/pharmacy")]
    public class PharmacyItemsController : ApiControllerBase
    {
        // GET: api/pharmacy/items
        [HttpGet("items")]
        [RequirePermission("Pharmacy", "Read")] 
        public async Task<IActionResult> GetPharmacyItems([FromQuery] GetPharmacyItemsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // POST: api/pharmacy/items
        [HttpPost("items")]
        [RequirePermission("Pharmacy", "Write")] 
        public async Task<IActionResult> SetupPharmacyItem([FromBody] SetupPharmacyItemCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // POST: api/pharmacy/suppliers
        [HttpPost("suppliers")]
        [RequirePermission("Pharmacy", "Write")] 
        public async Task<IActionResult> AddSupplier([FromBody] AddSupplierCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}