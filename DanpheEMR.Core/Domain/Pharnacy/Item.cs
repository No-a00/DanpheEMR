using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Pharmacy;

namespace DanpheEMR.Core.Domain.Pharnacy
{
    public class Item : BaseEntity
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string GernericName { get; set; }
        public string UOM { get; set; }        
        public int ReorderLevel { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
