
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Enums;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class GoodsReceipt : BaseEntity, IHasActiveStatus
    {
        public Guid Id { get; set; }
        public string GoodsReceiptNo { get; set; } 
        public DateTime ReceiptDate { get; set; } 
        public string InvoiceNo { get; set; } 
        public decimal TotalAmount { get; set; } // Tổng tiền của cả lô hàng
        public string Remarks { get; set; } // Ghi chú (VD: "Hàng nhập đợt 1 dự án ABC")
        public GoodsReceiptStatus Status { get; set; }
        //xóa mềm 
        public bool IsActive { get; set; }
        public string CancelReason { get; set; }
        public Guid? CancelledByUserId { get; set; }

        public Guid SupplierId { get; set; } // Mua của nhà cung cấp nào?
        public Guid StoreId { get; set; } // Nhập vào kho nào? (Thường là Kho Tổng)

        public Supplier Supplier { get; set; }    
        public Store Store { get; set; }
        // Một phiếu nhập tổng sẽ có danh sách nhiều mặt hàng chi tiết
        public ICollection<GoodsReceiptItem> GoodsReceiptItems { get; set; }
    }
}