using DanpheEMR.Application.Features.EMR.Commands.AddClinicalNote;
using DanpheEMR.Application.Features.EMR.Commands.AddDiagnosis;
using DanpheEMR.Application.Features.EMR.Commands.AddProgressNote;
using DanpheEMR.Application.Features.EMR.Commands.RecordVitals;
using DanpheEMR.Application.Features.EMR.Queries.GetPatientMedicalHistory;
using DanpheEMR.WEB.Security;
using Microsoft.AspNetCore.Mvc;


namespace DanpheEMR.WEB.Controllers.Clinical
{
    [Route("api/clinical-records")]
    public class ClinicalRecordsController : ApiControllerBase
    {
        // GET: api/clinical-records/patients/{patientId}/history
        [HttpGet("patients/{patientId}/history")]
        [RequirePermission("EMR", "Read")] 
        public async Task<IActionResult> GetPatientMedicalHistory(Guid patientId)
        {
            var result = await Mediator.Send(new GetPatientMedicalHistoryQuery(patientId));
            return Ok(result);
        }

        // POST: api/clinical-records/patients/{patientId}/vitals
        [HttpPost("patients/{patientId}/vitals")]
        [RequirePermission("EMR", "Write")] 
        public async Task<IActionResult> RecordVitals(Guid patientId, [FromBody] RecordVitalsCommand command)
        {
            if (patientId != command.PatientId) return BadRequest("Patient ID không khớp.");
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // POST: api/clinical-records/patients/{patientId}/diagnoses
        [HttpPost("patients/{patientId}/diagnoses")]
        [RequirePermission("EMR", "Write")] 
        public async Task<IActionResult> AddDiagnosis(Guid patientId, [FromBody] AddDiagnosisCommand command)
        {
            if (patientId != command.PatientId) return BadRequest("Patient ID không khớp.");
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // POST: api/clinical-records/notes
        [HttpPost("notes")]
        [RequirePermission("EMR", "Write")] 
        public async Task<IActionResult> AddClinicalNote([FromBody] AddClinicalNoteCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // POST: api/clinical-records/progress-notes
        [HttpPost("progress-notes")]
        [RequirePermission("EMR", "Write")] 
        public async Task<IActionResult> AddProgressNote([FromBody] AddProgressNoteCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}