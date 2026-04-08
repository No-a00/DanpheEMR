using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class StockTransferItem : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid StockTransferId { get; set; }
        public Guid ItemId { get; set; }
        public string BatchNo { get; set; }
        public int Quantity { get; set; }

        public virtual StockTransfer StockTransfer { get; set; }
        public virtual Item Item { get; set; }
    }
}