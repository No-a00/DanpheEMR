using DanpheEMR.Core.Domain.Appointment;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Pharnacy;

namespace DanpheEMR.Core.Domain.Pharmacy
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
