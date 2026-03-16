using DanpheEMR.Core.Domain.Base;
using System.Collections.Generic;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string CategoryCode { get; set; } // Mã nhóm cha (VD: "MED" cho Medicine, "CON" cho Consumables)
        public string CategoryName { get; set; } // Tên nhóm cha (VD: "Thuốc", "Vật tư y tế", "Hóa chất")
        public string Description { get; set; }
        public bool IsActive { get; set; } = true; // Trạng thái hoạt động

        // Một nhóm cha sẽ chứa rất nhiều Nhóm con (SubCategories)
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}