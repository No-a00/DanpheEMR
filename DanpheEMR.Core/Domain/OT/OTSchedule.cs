using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Nums;
using DanpheEMR.Core.Domain.Patients;

namespace DanpheEMR.Core.Domain.OT
{
    public class OTSchedule : BaseEntity
    {
        public int Id { get; set; }
        public DateTime SurgeryDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SurgeryType { get; set; }
        public OTStatus Status { get; set; } // Scheduled, Completed, Cancelled
        public string Remarks { get; set; }
        //khóa ngoại
        public int PatientId { get; set; }
        public int OTRoomId { get; set; }
        public int SurgeonId { get; set; }
        public int? AnesthetistId { get; set; }
        public int? AdmissionId { get; set; }

        public Employee Anesthetist { get; set; }
        public Patient Patient { get; set; }
        public OTRoom OTRoom { get; set; }
        public Employee Surgeon { get; set; }
    }
}
