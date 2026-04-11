using DanpheEMR.Application.Features.Appointments.Commands.AddHoliday;
using DanpheEMR.Application.Features.Appointments.Commands.SetupDoctorSchedule;
using DanpheEMR.Application.Features.Appointments.Queries.GetDoctorDailySchedule;
using DanpheEMR.Application.Features.Appointments.Queries.GetDoctorSchedule;
using DanpheEMR.Features.Appointment.Commands.AddHoliday;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DanpheEMR.WEB.Controllers.Appointments
{
    [Route("api/doctor-schedules")]
    public class DoctorSchedulesController : ApiControllerBase
    {
        // GET: api/doctor-schedules/{doctorId}
        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetDoctorSchedule(Guid doctorId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var query = new GetDoctorScheduleQuery(doctorId, startDate, endDate);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // GET: api/doctor-schedules/{doctorId}/daily?date=2023-10-25
        [HttpGet("{doctorId}/daily")]
        public async Task<IActionResult> GetDoctorDailySchedule(Guid doctorId, [FromQuery] DateTime date)
        {
            var query = new GetDoctorDailyScheduleQuery(doctorId, date);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // POST: api/doctor-schedules
        [HttpPost]
        public async Task<IActionResult> SetupDoctorSchedule([FromBody] SetupDoctorScheduleCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // POST: api/doctor-schedules/holidays
        [HttpPost("holidays")]
        public async Task<IActionResult> AddHoliday([FromBody] AddHolidayCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}