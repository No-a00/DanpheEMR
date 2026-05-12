using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class Store : BaseEntity,ISoftDelete
    {
        public Guid Id { get; set; }
        public string StoreName { get; set; } 
        public string StoreCode { get; set; } // Mã kho 
        public string Location { get; set; }  // Vị trí 
        public bool IsActive { get; set; } = true; // Kho có đang hoạt động không?7
        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }

       public string? DeletedBy { get; set; }

       public Guid UserId { get; set; } // Người có quyền quản lý kho này
        public User User { get; set; }
    }
}