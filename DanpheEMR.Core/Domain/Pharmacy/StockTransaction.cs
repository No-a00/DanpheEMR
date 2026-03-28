using DanpheEMR.Core.Domain.Base;


namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class StockTransaction : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; }

        // Loại giao dịch: "GoodsReceipt" (Nhập), "Dispatch" (Xuất), "Sale" (Bán lẻ), "Transfer" (Chuyển kho)
        public string TransactionType { get; set; }

        public string ReferenceNo { get; set; } // Mã chứng từ gốc (Ví dụ: số phiếu nhập GR-001, hoặc số đơn thuốc RX-999)

        public int InQty { get; set; } // Số lượng nhập vào (Nếu là giao dịch Xuất thì cột này = 0)
        public int OutQty { get; set; } // Số lượng xuất ra (Nếu là giao dịch Nhập thì cột này = 0)

        // Quan trọng: Giao dịch này tác động lên Lô nào? Của Thuốc nào? Ở Kho nào?
        public string BatchNo { get; set; }

        public Guid ItemId { get; set; }
        public Guid StoreId { get; set; }

        public Item Item { get; set; }

        
        public Store Store { get; set; }
    }
}