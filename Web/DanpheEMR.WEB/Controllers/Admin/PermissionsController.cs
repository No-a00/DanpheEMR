using DanpheEMR.Application.Features.Admin.Commands.AssignRolePermission;
using DanpheEMR.Application.Features.Admin.Commands.RemoveRolePermission;
using DanpheEMR.Application.Features.Admin.Queries.GetPermissions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DanpheEMR.WEB.Controllers.Admin
{
    [Route("api/admin/permissions")]
    public class PermissionsController : ApiControllerBase
    {
        // GET: api/admin/permissions
        [HttpGet]
        public async Task<IActionResult> GetPermissions([FromQuery] GetPermissionsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // POST: api/admin/permissions/assign
        [HttpPost("assign")]
        public async Task<IActionResult> AssignRolePermission([FromBody] AssignRolePermissionCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // POST: api/admin/permissions/remove
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveRolePermission([FromBody] RemoveRolePermissionCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}