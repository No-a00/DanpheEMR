
using DomainAppointment = DanpheEMR.Core.Domain.Appointments.Appointment;

namespace DanpheEMR.Application.Features.Appointments.Commands.RescheduleAppointment
{
    public static class RescheduleAppointmentMapping
    {
        public static void UpdateEntity(this RescheduleAppointmentCommand command, DomainAppointment appointment, Guid currentUserId)
        {

            appointment.AppointmentDate = command.NewAppointmentDate;
            appointment.AppointmentTime = command.NewAppointmentTime;
            appointment.Reason = command.Reason;
            appointment.ReasonUserId = currentUserId;
        }
    }
}