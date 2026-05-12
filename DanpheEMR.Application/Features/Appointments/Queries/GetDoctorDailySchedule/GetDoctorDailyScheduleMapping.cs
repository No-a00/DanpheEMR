
using DomainAppointment = DanpheEMR.Core.Domain.Appointments.Appointment;

namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorDailySchedule
{
    public static class GetDoctorDailyScheduleMapping
    {
        public static AppointmentItemDto ToDto(this DomainAppointment appointment)
        {
            return new AppointmentItemDto
            {
                Code = appointment.AppointmentCode,
                AppointmentTime = appointment.AppointmentTime,
                PatientName = appointment.Patient != null ? $"{appointment.Patient.FirstName} {appointment.Patient.LastName}" : "Bệnh nhân ẩn danh",

                Status = appointment.Status,
                Reason = appointment.Reason,
                IsActive = appointment.IsActive
            };
        }
        public static List<AppointmentItemDto> ToDtoList(this IEnumerable<DomainAppointment> appointments)
        {
            return appointments.Select(a => a.ToDto()).ToList();
        }
    }
}