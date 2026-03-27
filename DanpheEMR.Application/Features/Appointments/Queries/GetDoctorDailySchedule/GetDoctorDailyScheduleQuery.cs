using MediatR;

namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorDailySchedule
{
    public record GetDoctorDailyScheduleQuery(
        Guid DoctorId,
        DateTime Date
    ) : IRequest<Result<GetDoctorDailyScheduleResponse>>;
}