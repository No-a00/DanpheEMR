using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class Item : BaseEntity, IHasActiveStatus
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string GernericName { get; set; }
        public string UOM { get; set; }        
        public int ReorderLevel { get; set; }
        //xóa mềm 
        public bool IsActive { get; set; }
        public string CancelReason { get; set; }
        public int CancelUserId { get; set; }

        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
