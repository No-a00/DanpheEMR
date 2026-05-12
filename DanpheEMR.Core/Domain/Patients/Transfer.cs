
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Enums;


namespace DanpheEMR.Core.Domain.Patients
{
    public class Transfer : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }
        public DateTime TransferDate { get; set; }// Ngày giờ chuyển khoa
        public string? TransferReason { get; set; } 
        public TransferStatus TransferStatus { get; set; }
        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }
       public string? DeletedBy { get; set; }

        public Guid AdmissionId { get; set; }
        public Guid FromDeptId { get; set; }
        public Guid ToDeptId { get; set; }
        public  Admission Admission { get; set; } 
        public Department Department { get; set; } 
    }
}
