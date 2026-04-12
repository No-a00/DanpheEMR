
using MediatR;

namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorSchedule
{
    public sealed record GetDoctorScheduleQuery(Guid DoctorId, DateTime? StartDate, DateTime? EndDate) 
        : IRequest<Result<List<DoctorScheduleResponse>>>;
}