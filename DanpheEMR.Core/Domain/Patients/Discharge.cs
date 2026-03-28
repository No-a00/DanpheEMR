using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Patients
{
    public class Discharge : BaseEntity, IHasActiveStatus
    {
        public Guid Id { get; set; }
        public DateTime DischargeDate { get; set; }
        public string DischargeCondition { get; set; }
        public string DischargeNotes { get; set; }

        //hủy và lí do
        public bool IsActive { get; set; }
        public string VoidReason { get; set; }
        public Guid VoidedByUserId { get; set; }
        //
        public Guid PatientId { get; set; }
        public Guid AdmissionId { get; set; }
        public Patient Patient { get; set; }
        public Admission Admission { get; set; }
    }
}
