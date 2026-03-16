using DanpheEMR.Core.Domain.Base;
using System;
using System.Collections.Generic;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class GoodsReceipt : BaseEntity
    {
        public int Id { get; set; }
        public string GoodsReceiptNo { get; set; } 
        public DateTime ReceiptDate { get; set; } 
        public string InvoiceNo { get; set; } 
        public decimal TotalAmount { get; set; } // Tổng tiền của cả lô hàng
        public string Remarks { get; set; } // Ghi chú (VD: "Hàng nhập đợt 1 dự án ABC")

        // Trạng thái: Pending (Đang nháp), Approved (Đã duyệt - lúc này mới cộng vào bảng Stock), Cancelled
        public string Status { get; set; }

        public int SupplierId { get; set; } // Mua của nhà cung cấp nào?
        public Supplier Supplier { get; set; }

        public int StoreId { get; set; } // Nhập vào kho nào? (Thường là Kho Tổng)
        public Store Store { get; set; }

        // Một phiếu nhập tổng sẽ có danh sách nhiều mặt hàng chi tiết
        public ICollection<GoodsReceiptItem> GoodsReceiptItems { get; set; }
    }
}