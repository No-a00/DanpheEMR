
using DomainAppointment = DanpheEMR.Core.Domain.Appointments.Appointment;

namespace DanpheEMR.Application.Features.Appointments.Commands.RescheduleAppointment
{
    public static class RescheduleAppointmentMapping
    {
        public static void UpdateEntity(this RescheduleAppointmentCommand command, DomainAppointment appointment, string currentUserCode)
        {

            appointment.AppointmentDate = command.NewAppointmentDate;
            appointment.AppointmentTime = command.NewAppointmentTime;

            appointment.IsDeleted = true;
            appointment.Reason = command.Reason;
            appointment.DeletedBy = currentUserCode;  
        }
    }
}