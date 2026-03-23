using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class Store : BaseEntity,IHasActiveStatus
    {
        public Guid Id { get; set; }
        public string StoreName { get; set; } 
        public string StoreCode { get; set; } // Mã kho (VD: "KHO-TONG")
        public string Location { get; set; }  // Vị trí (VD: "Tầng 1 - Khu A")
        public bool IsActive { get; set; } = true; // Kho có đang hoạt động không?
        public string CancelReason { get; set; }
        public int CancelledByUserId { get; set; }

        // (Tùy chọn) Nếu bạn muốn phân quyền, có thể thêm danh sách các User được phép vào kho này
    }
}