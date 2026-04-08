using DanpheEMR.Core.Domain.Base;

using System.Collections.Generic;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class SubCategory : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }
        public string SubCategoryCode { get; set; } // Mã nhóm con (VD: "GD-01")
        public string SubCategoryName { get; set; } // Tên nhóm
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string Reason { get; set; }

        public Guid? DeletedBy { get; set; }

        public string CancelReason { get; set; }
        public Guid? CancelledByUserId { get; set; }
        public Guid CategoryId { get; set; } 
        public Category Category { get; set; } 
        public ICollection<Item> Items { get; set; }
    }
}