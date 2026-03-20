using DanpheEMR.Core.Domain.Base;
namespace DanpheEMR.Core.Domain.Wards
{
    public class BedFeature : BaseEntity
    {
        public int Id { get; set; }
        public string FeatureCode { get; set; } // Mã loại (VD: "VIP", "STD", "ICU")
        public string FeatureName { get; set; } // Tên loại (VD: "Giường VIP 1 người", "Giường Thường")
        public string Description { get; set; }
        public decimal BedPrice { get; set; }

        public bool IsActive { get; set; }
        public string CancelReason { get; set; }
        public int? CancelledByUserId { get; set; }

        // Navigation Property: Một loại giường (VD: VIP) được áp dụng cho nhiều cái Giường thực tế khác nhau
        public ICollection<Bed> Beds { get; set; }
    }
}