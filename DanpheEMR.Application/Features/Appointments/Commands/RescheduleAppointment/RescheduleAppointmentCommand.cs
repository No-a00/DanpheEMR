using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Appointments.Commands.RescheduleAppointment
{
    public record RescheduleAppointmentCommand(
        string AppointmentCode,
        DateTime NewAppointmentDate,
        TimeSpan NewAppointmentTime,
        string Reason
    ) : IRequest<Result<Guid>>;
}