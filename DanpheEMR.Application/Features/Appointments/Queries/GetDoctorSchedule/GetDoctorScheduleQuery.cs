
using MediatR;

namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorSchedule
{
    public sealed record GetDoctorScheduleQuery(Guid DoctorId, DateTime? startDate, DateTime? endDate)
        : IRequest<Result<List<DoctorScheduleResponse>>>;
}