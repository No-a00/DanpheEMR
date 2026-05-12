using DanpheEMR.Application.Features.Admin.Commands.CreateRole;
using DanpheEMR.Application.Features.Admin.Commands.DeleteRole;
using DanpheEMR.Application.Features.Admin.Commands.UpdateRole;
using DanpheEMR.Application.Features.Admin.Queries.GetRoleDetails;
using DanpheEMR.Application.Features.Admin.Queries.GetRoles;
using DanpheEMR.WEB.Security; 
using Microsoft.AspNetCore.Mvc;

namespace DanpheEMR.WEB.Controllers.Admin
{
    [Route("api/admin/roles")]
    public class RolesController : ApiControllerBase
    {
        // GET: api/admin/roles
        [HttpGet]
        [RequirePermission("Admin", "Read")] 
        public async Task<IActionResult> GetRoles([FromQuery] GetRolesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // GET: api/admin/roles/{id}
        [HttpGet("{id}")]
        [RequirePermission("Admin", "Read")] 
        public async Task<IActionResult> GetRoleDetails(string Code)
        {
            var result = await Mediator.Send(new GetRoleDetailsQuery(Code));
            return Ok(result);
        }

        // POST: api/admin/roles
        [HttpPost]
        [RequirePermission("Admin", "Write")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // PUT: api/admin/roles/{id}
        [HttpPut("{id}")]
        [RequirePermission("Admin", "Write")] 
        public async Task<IActionResult> UpdateRole(string Code, [FromBody] UpdateRoleCommand command)
        {
            if (Code != command.RoleName)
            {
                return BadRequest(new { Message = "Code không khớp với dữ liệu." });
            }

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // DELETE: api/admin/roles/{id}
        [HttpDelete("{id}")]
        [RequirePermission("Admin", "Full")] 
        public async Task<IActionResult> DeleteRole(string Code)
        {
            var result = await Mediator.Send(new DeleteRoleCommand(Code));
            return Ok(result);
        }
    }
}