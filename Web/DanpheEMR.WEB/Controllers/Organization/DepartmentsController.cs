using DanpheEMR.Application.Features.Admin.Commands.SetupBranch;
using DanpheEMR.Application.Features.Admin.Commands.SetupDepartment;
using DanpheEMR.Application.Features.Admin.Queries.GetDepartmentTree;
using Microsoft.AspNetCore.Mvc;


namespace DanpheEMR.WEB.Controllers.Organization
{
    [Route("api/organization")]
    public class DepartmentsController : ApiControllerBase
    {
        // GET: api/organization/departments/tree
        [HttpGet("departments/tree")]
        public async Task<IActionResult> GetDepartmentTree([FromQuery] GetDepartmentTreeQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // POST: api/organization/departments
        [HttpPost("departments")]
        public async Task<IActionResult> SetupDepartment([FromBody] SetupDepartmentCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // POST: api/organization/branches
        [HttpPost("branches")]
        public async Task<IActionResult> SetupBranch([FromBody] SetupBranchCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}