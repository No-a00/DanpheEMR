using DanpheEMR.Core.Domain.Base;
using System.Collections.Generic;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class Category : BaseEntity, IHasActiveStatus
    {
        public Guid Id { get; set; }

        public string CategoryCode { get; set; } // Mã nhóm cha (VD: "MED", "CON")
        public string CategoryName { get; set; } // Tên nhóm cha (VD: "Thuốc", "Vật tư y tế")
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;
        public string CancelReason { get; set; }
        public Guid? UserIdCancel { get; set; } 
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}