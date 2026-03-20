using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Nums;

namespace DanpheEMR.Core.Domain.Patients
{
    public class Transfer : BaseEntity
    {
        public int Id { get; set; }
        public DateTime TransferDate { get; set; }// Ngày giờ chuyển khoa
        public string? Reason { get; set; } // Lý do chuyển khoa
        public TransferStatus TransferStatus { get; set; }
        // xóa mềm
        public bool IsActive { get; set; }
        public string cancelReason { get; set; }
        public int voidedByUserId {  get; set; }
        public int AdmissionId { get; set; } // Khóa ngoại đến bảng Admission
        public int FromDeptId { get; set; }
        public int ToDeptId { get; set; }
        public  Admission Admission { get; set; } // Navigation property đến Admission
        public Department FromDepartment { get; set; } // Navigation property đến Department (khoa chuyển đi)
        public Department ToDepartment { get; set; } // Navigation property đến Department (khoa chuyển đến)
    }
}
