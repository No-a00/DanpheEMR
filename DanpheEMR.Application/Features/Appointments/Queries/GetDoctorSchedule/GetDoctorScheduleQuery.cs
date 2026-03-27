
using MediatR;

namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorSchedule
{
    public sealed record GetDoctorScheduleQuery(Guid DoctorId, DateTime Date)
        : IRequest<Result<List<DoctorScheduleResponse>>>;
}