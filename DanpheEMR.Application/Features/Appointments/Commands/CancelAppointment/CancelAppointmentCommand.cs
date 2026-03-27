using MediatR;
namespace DanpheEMR.Application.Features.Appointments.Commands.CancelAppointment
{
    public record CancelAppointmentCommand(
        Guid Id,
        string CancelReason
    ) : IRequest<Result<Guid>>;
}