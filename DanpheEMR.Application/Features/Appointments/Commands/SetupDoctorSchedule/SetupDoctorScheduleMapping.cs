using System;
using DanpheEMR.Core.Domain.Appointments;

namespace DanpheEMR.Application.Features.Appointments.Commands.SetupDoctorSchedule
{
    public static class SetupDoctorScheduleMapping
    {
        public static DoctorSchedule ToEntity(this SetupDoctorScheduleCommand command)
        {
            return new DoctorSchedule
            {
                Id = Guid.NewGuid(),
                DayOfWeek = command.DayOfWeek, // Truyền thẳng giá trị Thứ vào đây
                StartTime = command.StartTime,
                EndTime = command.EndTime,
                MaxPatients = command.MaxPatients,
                ProviderId = command.ProviderId,
                DepartmentId = command.DepartmentId
            };
        }
    }
}