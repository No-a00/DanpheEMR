using System;

namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorSchedule
{
    public sealed class DoctorScheduleResponse
    {
        public string DoctorScheduleCode { get; set; }
        public string StartTime { get; set; } 
        public string EndTime { get; set; }
        public int MaxPatients { get; set; }
        // Có thể thêm thông tin bác sĩ hoặc khoa nếu cần
    }
}