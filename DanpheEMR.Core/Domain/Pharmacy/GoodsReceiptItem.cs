using DanpheEMR.Core.Domain.Base;
using System;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class GoodsReceiptItem : BaseEntity
    {
        public Guid Id { get; set; }
        public string BatchNo { get; set; } //  Lô số bao nhieu
        public DateTime ExpiryDate { get; set; } //  Hạn sử dụng 

        public int ReceivedQuantity { get; set; } // Số lượng nhập 
        public int FreeQuantity { get; set; } // Số lượng hàng tặng 

        public decimal PurchaseRate { get; set; } // Giá mua vào
        public decimal Margin { get; set; } // Tỷ lệ phần trăm lợi nhuận
        public decimal SellingPrice { get; set; } // Giá bán ra 
        public decimal SubTotal { get; set; } // Thành tiền 

        public Guid GoodsReceiptId { get; set; }
        public Guid ItemId { get; set; }
        public GoodsReceipt GoodsReceipt { get; set; }

        
        public Item Item { get; set; }
    }
}