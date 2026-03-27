
using DomainAppointment = DanpheEMR.Core.Domain.Appointments.Appointment;

namespace DanpheEMR.Application.Features.Appointments.Queries.GetPatientAppointments
{
    public static class GetPatientAppointmentsMapping
    {
        public static PatientAppointmentItemDto ToDto(this DomainAppointment appointment)
        {
            return new PatientAppointmentItemDto
            {
                Id = appointment.Id,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,

                DoctorName = appointment.Provider != null ? $"{appointment.Provider.FirstName} {appointment.Provider.LastName}" : "N/A",
                DepartmentName = appointment.Department != null ? appointment.Department.DepartmentName : "N/A",

                Status = appointment.Status,
                Reason = appointment.Reason,
                IsActive = appointment.IsActive
            };
        }

        public static List<PatientAppointmentItemDto> ToDtoList(this IEnumerable<DomainAppointment> appointments)
        {
            return appointments.Select(a => a.ToDto()).ToList();
        }
    }
}