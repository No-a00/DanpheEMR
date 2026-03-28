using DanpheEMR.Core.Domain.Base;

using System.Collections.Generic;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class SubCategory : BaseEntity, IHasActiveStatus
    {
        public Guid Id { get; set; }
        public string SubCategoryCode { get; set; } // Mã nhóm con (VD: "GD-01")
        public string SubCategoryName { get; set; } // Tên nhóm (VD: "Giảm đau - Hạ sốt")
        public string Description { get; set; }
        public bool IsActive { get; set; } = true; // Trạng thái hoạt động

        public string CancelReason { get; set; }
        public Guid? CancelledByUserId { get; set; }
        public Guid CategoryId { get; set; } // Khóa ngoại trỏ lên nhóm Cha
        public Category Category { get; set; } // Navigation property lên Category

        // Một nhóm con sẽ chứa rất nhiều Mặt hàng (Items)
        public ICollection<Item> Items { get; set; }
    }
}