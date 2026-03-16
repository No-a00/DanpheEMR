using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Appointment
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
    }
}
