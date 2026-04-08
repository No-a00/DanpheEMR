using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class StockTransfer : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid FromStoreId { get; set; }
        public Guid ToStoreId { get; set; }
        public DateTime TransferDate { get; set; }
        public string Remarks { get; set; }
        public string TransferStatus { get; set; } 

        public virtual Store FromStore { get; set; }
        public virtual Store ToStore { get; set; }
        public virtual ICollection<StockTransferItem> Items { get; set; }
    }
}