using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Admin; 
namespace DanpheEMR.Core.Domain.Billing
{
    public class BillingTransactionItem : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        // --- KHÓA NGOẠI LIÊN KẾT VỚI HÓA ĐƠN TỔNG ---
        public int BillingTransactionId { get; set; }
        public virtual BillingTransaction BillingTransaction { get; set; }

        // --- KHÓA NGOẠI LIÊN KẾT VỚI DANH MỤC DỊCH VỤ ---
        // Ánh xạ đến dịch vụ gốc trong bảng ServiceItem (VD: Xét nghiệm máu, X-Quang)
        public int ServiceItemId { get; set; }
        public virtual ServiceItem ServiceItem { get; set; }

        // --- SNAPSHOT DỮ LIỆU (Bắt buộc phải có trong Kế toán) ---
        // Copy Tên dịch vụ tại thời điểm xuất hóa đơn
        [Required, MaxLength(255)]
        public string ItemName { get; set; }

        // --- THÔNG TIN TÀI CHÍNH TRÊN TỪNG DÒNG ---
        public float Quantity { get; set; } // Số lượng (VD: 1 lần khám, hoặc 2 viên thuốc)

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Đơn giá tại thời điểm mua

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; } // = Quantity * Price

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; } // Tiền giảm giá cho riêng dịch vụ này

        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; } // Thuế cho riêng dịch vụ này

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; } // = SubTotal - DiscountAmount + TaxAmount

        // --- THÔNG TIN PHỤC VỤ HOA HỒNG (COMISSION) ---
        // Bác sĩ nào là người trực tiếp thực hiện hoặc chỉ định dịch vụ này?
        public int? ProviderId { get; set; }
        public virtual Employee Provider { get; set; }
    }
}