using DanpheEMR.Application.Features.Appointments.Commands.BookAppointment;
using DanpheEMR.Application.Features.Appointments.Commands.CancelAppointment;
using DanpheEMR.Application.Features.Appointments.Commands.RescheduleAppointment;
using DanpheEMR.Application.Features.Appointments.Queries.GetPatientAppointments;
using DanpheEMR.WEB.Security; 
using Microsoft.AspNetCore.Mvc;


namespace DanpheEMR.WEB.Controllers.Appointments
{
    [Route("api/appointments")]
    public class AppointmentsController : ApiControllerBase
    {
        // GET: api/appointments/patient/{patientId}
        [HttpGet("patient/{patientId}")]
        [RequirePermission("Appointment", "Read")] 
        public async Task<IActionResult> GetPatientAppointments(Guid patientId)
        {
            var result = await Mediator.Send(new GetPatientAppointmentsQuery(patientId));
            return Ok(result);
        }

        // POST: api/appointments
        [HttpPost]
        [RequirePermission("Appointment", "Write")]
        public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // PUT: api/appointments/{id}/reschedule
        [HttpPut("{id}/reschedule")]
        [RequirePermission("Appointment", "Write")] 
        public async Task<IActionResult> RescheduleAppointment(Guid id, [FromBody] RescheduleAppointmentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(new { Message = "ID cuộc hẹn không khớp." });
            }

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // PUT: api/appointments/{id}/cancel
        [HttpPut("{id}/cancel")]
        [RequirePermission("Appointment", "Write")] 
        public async Task<IActionResult> CancelAppointment(Guid id, [FromBody] CancelAppointmentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(new { Message = "ID cuộc hẹn không khớp." });
            }

            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}