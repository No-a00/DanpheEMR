using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Patients
{
    public class PatientGuarantee : BaseEntity, IHasActiveStatus
    {
        public Guid Id { get; set; }
        public string GuarantorName { get; set; }
        public string IDCardNumber { get; set; }
        public string Relationship { get; set; }
        public decimal? GuaranteeAmount { get; set; }
        
        public string Address { get; set; }
        //hủy và lí do
        public bool IsActive { get; set; }
        public string CancelReason { get; set; }
        public int CancelledByUserId { get; set; }

        //
        public Guid patientId { get; set; }
        public Patient Patient { get; set; }
    }
}
