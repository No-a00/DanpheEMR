using DanpheEMR.Core.Domain.Base;
using System;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class StockTransaction : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; }

    
        public string TransactionType { get; set; }

        public string ReferenceNo { get; set; }

        public int InQty { get; set; }
        public int OutQty { get; set; }
        public int StockBalance { get; set; }

        public string BatchNo { get; set; }
        public Guid ItemId { get; set; }
        public Guid StoreId { get; set; }


        public virtual Item Item { get; set; }
        public virtual Store Store { get; set; }
    }
}