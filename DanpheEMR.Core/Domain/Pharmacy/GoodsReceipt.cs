
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Enums;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public class GoodsReceipt : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }
        public string GoodsReceiptNo { get; set; } 
        public DateTime ReceiptDate { get; set; } 
        public string InvoiceNo { get; set; } 
        public decimal TotalAmount { get; set; } // Tổng tiền 
        public string Remarks { get; set; } // Ghi chú 
        public GoodsReceiptStatus Status { get; set; }
        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string Reason { get; set; }
        public Guid? DeletedBy { get; set; }

        public Guid SupplierId { get; set; } //  nhà cung cấp 
        public Guid StoreId { get; set; } //   kho nhập

        public Supplier Supplier { get; set; }    
        public Store Store { get; set; }
        // mặt hàng chi tiết
        public ICollection<GoodsReceiptItem> GoodsReceiptItems { get; set; }
    }
}