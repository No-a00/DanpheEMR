using DanpheEMR.Core.Domain.Appointment;
using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Radiology
{
    public class Stock : BaseEntity
    {
        public int Id { get; set; }
        public string BatchNo { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int StoreId { get; set; }
    }
}
