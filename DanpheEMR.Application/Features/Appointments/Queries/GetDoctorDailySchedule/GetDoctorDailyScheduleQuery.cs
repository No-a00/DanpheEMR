using MediatR;

namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorDailySchedule
{
    public record GetDoctorDailyScheduleQuery(
        string DoctorCode,
        DateTime Date
    ) : IRequest<Result<GetDoctorDailyScheduleResponse>>;
}