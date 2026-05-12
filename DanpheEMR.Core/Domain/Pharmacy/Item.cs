using DanpheEMR.Core.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class Item : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string GenericName { get; set; }
        public string UOM { get; set; }        
        public int ReorderLevel { get; set; }
        // Thông tin xóa mềm

        public bool IsDeleted { get; set; }
        
        public string? Reason { get; set; }
        
       public string? DeletedBy { get; set; }

        public Guid SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
