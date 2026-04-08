using DanpheEMR.Core.Domain.Base;
using System;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class DispenseItem : BaseEntity
    {
        public Guid Id { get; set; }

        public Guid ItemId { get; set; }

        public string BatchNo { get; set; }
        public DateTime ExpiryDate { get; set; }

        public int Quantity { get; set; }
        public decimal SalePrice { get; set; }
        public decimal SubTotal { get; set; }

        public Guid DispenseId { get; set; }
        public virtual Dispense Dispense { get; set; }

        public virtual Item Item { get; set; }
    }
}