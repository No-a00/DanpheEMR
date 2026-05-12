
using MediatR;

namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorSchedule
{
    public sealed record GetDoctorScheduleQuery(string DoctorCode, DateTime? StartDate, DateTime? EndDate) 
        : IRequest<Result<List<DoctorScheduleResponse>>>;
}