using MediatR;
namespace DanpheEMR.Application.Features.Appointments.Commands.CancelAppointment
{
    public record CancelAppointmentCommand(
        string AppointmentCode,
        string CancelReason
    ) : IRequest<Result<string>>;
}