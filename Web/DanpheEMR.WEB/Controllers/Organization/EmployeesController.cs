using DanpheEMR.Application.Features.Admin.Commands.RegisterEmployee;
using DanpheEMR.Application.Features.Admin.Queries.GetEmployees;
using Microsoft.AspNetCore.Mvc;

namespace DanpheEMR.WEB.Controllers.Organization
{
    [Route("api/employees")]
    public class EmployeesController : ApiControllerBase
    {
        // GET: api/employees
        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] GetEmployeesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // POST: api/employees/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}