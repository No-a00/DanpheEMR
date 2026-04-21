using DanpheEMR.Application.Features.Admin.Queries.GetSystemParams;
using DanpheEMR.WEB.Security; 
using Microsoft.AspNetCore.Mvc;
namespace DanpheEMR.WEB.Controllers.Organization
{
    [Route("api/system-settings")]
    public class SystemSettingsController : ApiControllerBase
    {
        // GET: api/system-settings/params
        [HttpGet("params")]
        [RequirePermission("Base", "Read")] 
        public async Task<IActionResult> GetSystemParams([FromQuery] GetSystemParamsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}