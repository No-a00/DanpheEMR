using DanpheEMR.Application.Features.Admin.Commands.CreateRole;
using DanpheEMR.Application.Features.Admin.Commands.DeleteRole;
using DanpheEMR.Application.Features.Admin.Commands.UpdateRole;
using DanpheEMR.Application.Features.Admin.Queries.GetRoleDetails;
using DanpheEMR.Application.Features.Admin.Queries.GetRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DanpheEMR.WEB.Controllers.Admin
{
    [Route("api/admin/roles")]
    [Authorize(Roles = "SuperAdmin")]
    public class RolesController : ApiControllerBase
    {
        // GET: api/admin/roles
        [HttpGet]
        public async Task<IActionResult> GetRoles([FromQuery] GetRolesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // GET: api/admin/roles/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleDetails(Guid id)
        {
           
            var result = await Mediator.Send(new GetRoleDetailsQuery (id));
            return Ok(result);
        }

        // POST: api/admin/roles
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // PUT: api/admin/roles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] UpdateRoleCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(new { Message = "ID không khớp với dữ liệu." });
            }

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // DELETE: api/admin/roles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var result = await Mediator.Send(new DeleteRoleCommand (id));
            return Ok(result);
        }
    }
}