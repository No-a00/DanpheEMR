using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Pharnacy;
using System;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class GoodsReceiptItem : BaseEntity
    {
        public int Id { get; set; }
        public string BatchNo { get; set; } // Bắt buộc: Lô số mấy?
        public DateTime ExpiryDate { get; set; } // Bắt buộc: Hạn sử dụng ngày nào?

        public int ReceivedQuantity { get; set; } // Số lượng nhập thực tế
        public int FreeQuantity { get; set; } // Số lượng hàng tặng kèm (nếu có)

        public decimal PurchaseRate { get; set; } // Giá mua vào
        public decimal Margin { get; set; } // Tỷ lệ phần trăm lợi nhuận mong muốn
        public decimal SellingPrice { get; set; } // Giá bán ra (Thường tự động tính = PurchaseRate + Margin)
        public decimal SubTotal { get; set; } // Thành tiền (ReceivedQuantity * PurchaseRate)

        public int GoodsReceiptId { get; set; }
        public GoodsReceipt GoodsReceipt { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}