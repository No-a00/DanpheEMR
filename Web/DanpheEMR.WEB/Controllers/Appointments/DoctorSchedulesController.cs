using DanpheEMR.Application.Features.Appointments.Commands.SetupDoctorSchedule;
using DanpheEMR.Application.Features.Appointments.Queries.GetDoctorDailySchedule;
using DanpheEMR.Application.Features.Appointments.Queries.GetDoctorSchedule;
using DanpheEMR.Features.Appointment.Commands.AddHoliday;
using DanpheEMR.WEB.Security; 
using Microsoft.AspNetCore.Mvc;
namespace DanpheEMR.WEB.Controllers.Appointments
{
    [Route("api/doctor-schedules")]
    public class DoctorSchedulesController : ApiControllerBase
    {
        // GET: api/doctor-schedules/{doctorCode}
        [HttpGet("{doctorCode}")]
        [RequirePermission("Appointment", "Read")] 
        public async Task<IActionResult> GetDoctorSchedule(string doctorCode, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var query = new GetDoctorScheduleQuery(doctorCode, startDate, endDate);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // GET: api/doctor-schedules/{doctorCode}/daily?date=2023-10-25
        [HttpGet("{doctorCode}/daily")]
        [RequirePermission("Appointment", "Read")] 
        public async Task<IActionResult> GetDoctorDailySchedule(string doctorCode, [FromQuery] DateTime date)
        {
            var query = new GetDoctorDailyScheduleQuery(doctorCode, date);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // POST: api/doctor-schedules
        [HttpPost]
        [RequirePermission("Appointment", "Write")] 
        public async Task<IActionResult> SetupDoctorSchedule([FromBody] SetupDoctorScheduleCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // POST: api/doctor-schedules/holidays
        [HttpPost("holidays")]
        [RequirePermission("Appointment", "Write")] 
        public async Task<IActionResult> AddHoliday([FromBody] AddHolidayCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}