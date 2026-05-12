using DanpheEMR.Core.Domain.Base;
using System.Collections.Generic;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class Category : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }

        public string CategoryCode { get; set; } // Mã nhóm cha (VD: "MED", "CON")
        public string CategoryName { get; set; } // Tên nhóm cha (VD: "Thuốc", "Vật tư y tế")
        public string Description { get; set; }

        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }
       public string? DeletedBy { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}