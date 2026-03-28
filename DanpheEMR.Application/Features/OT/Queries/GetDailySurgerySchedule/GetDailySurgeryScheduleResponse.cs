using System;

namespace DanpheEMR.Application.Features.OT.Queries.GetDailySurgerySchedule
{
    public class GetDailySurgeryScheduleResponse
    {
        public Guid Id { get; set; }
        public DateTime SurgeryDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SurgeryType { get; set; }
        public string Status { get; set; } 
        public string Remarks { get; set; }
        public string RoomName { get; set; }
        public string SurgeonName { get; set; }
        public string PatientName { get; set; }
    }
}