using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Patients
{
    public class PatientGuarantee : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }
        public string GuarantorName { get; set; }
        public string IDCardNumber { get; set; }
        public string Relationship { get; set; }
        public decimal? GuaranteeAmount { get; set; }
        
        public string Address { get; set; }
        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string Reason { get; set; }
        public Guid? DeletedBy { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
