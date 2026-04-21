using DanpheEMR.Application.Features.Patients.Commands.RegisterPatient;
using DanpheEMR.Application.Features.Patients.Commands.UpdatePatientInfo;
using DanpheEMR.Application.Features.Patients.Queries.GetPatientById;
using DanpheEMR.Application.Features.Patients.Queries.GetPatientHistory;
using DanpheEMR.Application.Features.Patients.Queries.SearchPatients;
using DanpheEMR.WEB.Security; 
using Microsoft.AspNetCore.Mvc;


namespace DanpheEMR.WEB.Controllers.Patients
{
    [Route("api/patients")]
    public class PatientsController : ApiControllerBase
    {
        // GET: api/patients/search?name=Nguyen&phone=090...
        [HttpGet("search")]
        [RequirePermission("Patient", "Read")]
        public async Task<IActionResult> SearchPatients([FromQuery] SearchPatientsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // GET: api/patients/{id}
        [HttpGet("{id}")]
        [RequirePermission("Patient", "Read")]
        public async Task<IActionResult> GetPatientById(Guid id)
        {
            var result = await Mediator.Send(new GetPatientByIdQuery(id));
            return Ok(result);
        }

        // GET: api/patients/{id}/history
        [HttpGet("{id}/history")]
        [RequirePermission("Patient", "Read")]
        public async Task<IActionResult> GetPatientHistory(Guid id)
        {
            var result = await Mediator.Send(new GetPatientHistoryQuery(id));
            return Ok(result);
        }

        // POST: api/patients
        [HttpPost]
        [RequirePermission("Patient", "Write")] 
        public async Task<IActionResult> RegisterPatient([FromBody] RegisterPatientCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // PUT: api/patients/{id}
        [HttpPut("{id}")]
        [RequirePermission("Patient", "Write")] 
        public async Task<IActionResult> UpdatePatientInfo(Guid id, [FromBody] UpdatePatientInfoCommand command)
        {
            if (id != command.PatientId) return BadRequest("Patient ID không khớp.");

            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}