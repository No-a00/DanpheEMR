using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Patients
{
    public class PatientGuarantee : BaseEntity
    {
        public int Id { get; set; }
        public string GuarantorName { get; set; }
        public string IDCardNumber { get; set; }
        public string Relationship { get; set; }
        public decimal? GuaranteeAmount { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
