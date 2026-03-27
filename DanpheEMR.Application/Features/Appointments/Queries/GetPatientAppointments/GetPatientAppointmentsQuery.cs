
using MediatR;


namespace DanpheEMR.Application.Features.Appointments.Queries.GetPatientAppointments
{
    public record GetPatientAppointmentsQuery(
        Guid PatientId
    ) : IRequest<Result<GetPatientAppointmentsResponse>>;
}