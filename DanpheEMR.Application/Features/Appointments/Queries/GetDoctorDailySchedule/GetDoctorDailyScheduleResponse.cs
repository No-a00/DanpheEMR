using DanpheEMR.Core.Enums; // Nhớ check lại namespace Enum của bạn
using System;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorDailySchedule
{
    public class GetDoctorDailyScheduleResponse
    {
        public Guid DoctorId { get; set; }
        public DateTime Date { get; set; }
        public int TotalAppointments { get; set; }

        // Danh sách các lịch hẹn trong ngày
        public List<AppointmentItemDto> Appointments { get; set; } = new();
    }

    // Lớp DTO để hiển thị thông tin rút gọn cho từng dòng trên màn hình
    public class AppointmentItemDto
    {
        public Guid Id { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string PatientName { get; set; }
        public VisitStatus Status { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
    }
}