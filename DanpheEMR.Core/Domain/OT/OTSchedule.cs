using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;

namespace DanpheEMR.Core.Domain.OT
{
    public class OTSchedule : BaseEntity,ISoftDelete
    {
        public Guid Id { get; set; }
        public DateTime SurgeryDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SurgeryType { get; set; }
        public OTStatus Status { get; set; } // Scheduled, Completed, Cancelled
        public string Remarks { get; set; }
        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }
        public Guid? DeletedBy { get; set; }
  

        public Guid patientId { get; set; }
        public Guid OTRoomId { get; set; }
        public Guid SurgeonId { get; set; }
        public Guid? AnesthetistId { get; set; }
        public Guid? AdmissionId { get; set; }

        public Employee Anesthetist { get; set; }
        public Patient Patient { get; set; }
        public OTRoom OTRoom { get; set; }
        public Employee Surgeon { get; set; }
    }
}
