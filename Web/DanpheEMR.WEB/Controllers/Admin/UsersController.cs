using DanpheEMR.Application.Features.Admin.Commands.AssignUserRole;
using DanpheEMR.Application.Features.Admin.Commands.RemoveUserRole;
using DanpheEMR.Application.Features.Admin.Queries.GetUsersWithRoles;
using DanpheEMR.WEB.Security; 
using Microsoft.AspNetCore.Mvc;


namespace DanpheEMR.WEB.Controllers.Admin
{
    [Route("api/admin/users")]
    public class UsersController : ApiControllerBase
    {
        // GET: api/admin/users/with-roles
        [HttpGet("with-roles")]
        [RequirePermission("Admin", "Read")] 
        public async Task<IActionResult> GetUsersWithRoles([FromQuery] GetUsersWithRolesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // POST: api/admin/users/{userId}/roles
        [HttpPost("{userCode}/roles")]
        [RequirePermission("Admin", "Write")] 
        public async Task<IActionResult> AssignUserRole(string userCode, [FromBody] AssignUserRoleCommand command)
        {
            if (userCode != command.UserCode)
            {
                return BadRequest(new { Message = "UserCode không hợp lệ." });
            }

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // DELETE: api/admin/users/{userId}/roles
        [HttpDelete("{userId}/roles")]
        [RequirePermission("Admin", "Full")]
        public async Task<IActionResult> RemoveUserRole(string userCode, [FromBody] RemoveUserRoleCommand command)
        {
            if (userCode != command.UserCode)
            {
                return BadRequest(new { Message = "UserCode không hợp lệ." });
            }

            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}