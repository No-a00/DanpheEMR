using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class Item : BaseEntity, IHasActiveStatus
    {
        public Guid Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string GernericName { get; set; }
        public string UOM { get; set; }        
        public int ReorderLevel { get; set; }
        //xóa mềm 
        public bool IsActive { get; set; }
        public string CancelReason { get; set; }
        public Guid  CancelUserId { get; set; }

        public Guid SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
