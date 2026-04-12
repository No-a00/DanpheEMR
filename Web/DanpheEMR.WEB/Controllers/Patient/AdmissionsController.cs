
using DanpheEMR.Application.Features.Patients.Commands.AdmitPatient;
using DanpheEMR.Application.Features.Patients.Commands.CheckInPatient;
using DanpheEMR.Application.Features.Patients.Commands.DischargePatient;
using DanpheEMR.Application.Features.Patients.Commands.TransferPatient;
using DanpheEMR.Application.Features.Patients.Queries.GetAdmittedPatients;
using Microsoft.AspNetCore.Mvc;

namespace DanpheEMR.WEB.Controllers.Patients
{
    [Route("api/admissions")]
    public class AdmissionsController : ApiControllerBase
    {
        // GET: api/admissions/active
        [HttpGet("active")]
        public async Task<IActionResult> GetAdmittedPatients([FromQuery] GetAdmittedPatientsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // POST: api/admissions/check-in
        [HttpPost("check-in")]
        public async Task<IActionResult> CheckInPatient([FromBody] CheckInPatientCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // POST: api/admissions/admit
        [HttpPost("admit")]
        public async Task<IActionResult> AdmitPatient([FromBody] AdmitPatientCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // POST: api/admissions/{admissionId}/transfer
        [HttpPost("{admissionId}/transfer")]
        public async Task<IActionResult> TransferPatient(Guid admissionId, [FromBody] TransferPatientCommand command)
        {
            if (admissionId != command.AdmissionId) return BadRequest("Admission ID không khớp.");

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // PUT: api/admissions/{admissionId}/discharge
        [HttpPut("{admissionId}/discharge")]
        public async Task<IActionResult> DischargePatient(Guid admissionId, [FromBody] DischargePatientCommand command)
        {
            if (admissionId != command.AdmissionId) return BadRequest("Admission ID không khớp.");

            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}