using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Patients
{
    public class Discharge : BaseEntity
    {
        public int Id { get; set; }
        public DateTime DischargeDate { get; set; }
        public string DischargeCondition { get; set; }
        public string DischargeNotes { get; set; }

        //hủy và lí do
        public bool IsActive { get; set; }
        public string voidReason { get; set; }
        public int voidedByUserId { get; set; }
        //
        public int PatientId { get; set; }
        public int AdmissionId { get; set; }
        public Patient Patient { get; set; }
        public Admission Admission { get; set; }
    }
}
