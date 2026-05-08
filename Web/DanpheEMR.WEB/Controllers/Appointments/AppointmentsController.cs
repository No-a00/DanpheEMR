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
        // yêu cầu lịch hẹn khám bệnh của 1 bệnh nhân cụ thể
        [HttpGet("patient/{patientId}")]
        [RequirePermission("Appointment", "Read")] 
        public async Task<IActionResult> GetPatientAppointments(Guid patientId)
        {
            var result = await Mediator.Send(new GetPatientAppointmentsQuery(patientId));
            return Ok(result);
        }

        // POST: api/appointments
        // tạo lịch hẹn với doctor
        [HttpPost]
        [RequirePermission("Appointment", "Write")]
        public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // PUT: api/appointments/{id}/reschedule
        //thay đổi lịch hẹn 
        [HttpPut("{code}/reschedule")]
        [RequirePermission("Appointment", "Write")] 
        public async Task<IActionResult> RescheduleAppointment(string code, [FromBody] RescheduleAppointmentCommand command)
        {
            if (code != command.AppointmentCode)
            {
                return BadRequest(new { Message = "Mã  cuộc hẹn không khớp." });
            }

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // PUT: api/appointments/{id}/cancel
        //hủy cuộc hẹn 
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