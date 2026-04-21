using DanpheEMR.Application.Features.Pharmacy.Commands.ReceiveGoods;
using DanpheEMR.Application.Features.Pharmacy.Commands.TransferStock;
using DanpheEMR.Application.Features.Pharmacy.Queries.GetCurrentStock;
using DanpheEMR.WEB.Security; 
using Microsoft.AspNetCore.Mvc;
namespace DanpheEMR.WEB.Controllers.Pharmacy
{
    [Route("api/pharmacy/inventory")]
    public class PharmacyInventoryController : ApiControllerBase
    {
        // GET: api/pharmacy/inventory/stock
        [HttpGet("stock")]
        [RequirePermission("Pharmacy", "Read")] 
        public async Task<IActionResult> GetCurrentStock([FromQuery] GetCurrentStockQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // POST: api/pharmacy/inventory/receive
        // Nhập hàng từ nhà cung cấp
        [HttpPost("receive")]
        [RequirePermission("Pharmacy", "Write")] 
        public async Task<IActionResult> ReceiveGoods([FromBody] ReceiveGoodsCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // POST: api/pharmacy/inventory/transfer
        // Luân chuyển thuốc giữa Kho tổng và các Kho lẻ/Quầy thuốc
        [HttpPost("transfer")]
        [RequirePermission("Pharmacy", "Write")] 
        public async Task<IActionResult> TransferStock([FromBody] TransferStockCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}