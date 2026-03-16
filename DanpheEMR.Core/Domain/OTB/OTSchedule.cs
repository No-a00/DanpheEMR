using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Nums;

namespace DanpheEMR.Core.Domain.Appointment
{
    public class OTSchedule : BaseEntity
    {
        public int Id { get; set; }
        public DateTime SurgeryDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SurgeryType { get; set; }
        public VisitStatus Status { get; set; } // Scheduled, Completed, Cancelled
        public int PatientId { get; set; }
        public int OTRoomId { get; set; }
        public int SurgeonId { get; set; }
    }
}
