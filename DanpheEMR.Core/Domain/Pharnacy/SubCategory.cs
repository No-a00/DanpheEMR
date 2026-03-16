using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Pharnacy;
using System.Collections.Generic;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class SubCategory : BaseEntity
    {
        public int Id { get; set; }
        public string SubCategoryCode { get; set; } // Mã nhóm con (VD: "GD-01")
        public string SubCategoryName { get; set; } // Tên nhóm (VD: "Giảm đau - Hạ sốt")
        public string Description { get; set; }
        public bool IsActive { get; set; } = true; // Trạng thái hoạt động

        public int CategoryId { get; set; } // Khóa ngoại trỏ lên nhóm Cha
        public Category Category { get; set; } // Navigation property lên Category

        // Một nhóm con sẽ chứa rất nhiều Mặt hàng (Items)
        public ICollection<Item> Items { get; set; }
    }
}