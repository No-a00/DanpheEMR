using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Appointments.Commands.SetupDoctorSchedule
{
    public record SetupDoctorScheduleCommand(
        System.DayOfWeek DayOfWeek, // Đã đổi sang kiểu Thứ
        TimeSpan StartTime,
        TimeSpan EndTime,
        int MaxPatients,
        Guid ProviderId,
        Guid DepartmentId
    ) : IRequest<Result<Guid>>;
}