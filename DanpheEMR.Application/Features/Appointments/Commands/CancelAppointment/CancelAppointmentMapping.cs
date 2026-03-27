
using DanpheEMR.Core.Enums;
using DomainAppointment = DanpheEMR.Core.Domain.Appointments.Appointment;
namespace DanpheEMR.Application.Features.Appointments.Commands.CancelAppointment
{
    public static class CancelAppointmentMapping
    {
        public static void UpdateEntity(this CancelAppointmentCommand command, DomainAppointment appointment, Guid currentUserId)
        {
            appointment.IsActive = false;


            appointment.Status = VisitStatus.Cancelled;

            appointment.CancelReason = command.CancelReason;
            appointment.ReasonUserId = currentUserId;
        }
    }
}