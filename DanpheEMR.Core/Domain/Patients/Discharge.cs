using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Patients
{
    public class Discharge : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }
        public DateTime DischargeDate { get; set; }
        public string DischargeCondition { get; set; }
        public string DischargeNotes { get; set; }

        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }
        public Guid? DeletedBy { get; set; }

        public Guid PatientId { get; set; }
        public Guid AdmissionId { get; set; }
        public Patient Patient { get; set; }
        public Admission Admission { get; set; }
    }
}
