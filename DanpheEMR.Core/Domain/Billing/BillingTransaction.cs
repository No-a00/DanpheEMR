using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Nums;
using DanpheEMR.Core.Domain.Patients;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanpheEMR.Core.Domain.Billing
{
    public class BillingTransaction : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        // Mã hóa đơn hiển thị cho người dùng/kế toán (VD: INV-2026-0001)
        [Required, MaxLength(50)]
        public string InvoiceNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        [Required, MaxLength(50)]
        public PaymentStatus PaymentMode { get; set; }
        [Required, MaxLength(50)]
        public TransactionType TransactionType { get; set; }
        //Hủy hoặc cập nhật
        public string statusPayment { get; set; }
        public string cancelReason { get; set; }
        public bool isActive { get; set; }
        //

        // --- CÁC TRƯỜNG TIỀN TỆ (Quy định rõ decimal 18,2 để không sai số) ---
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        // Thay VisitStatus bằng một Enum chuyên dụng cho thanh toán (VD: Unpaid, Paid)
        [MaxLength(50)]
        public string PaymentStatus { get; set; } // Bạn có thể dùng Enum ở đây

        // Ghi chú của thu ngân
        [MaxLength(255)]
        public string Remarks { get; set; }

       
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int VisitId { get; set; }
        public Visit Visit { get; set; }

        // --- ĐIỂM QUAN TRỌNG NHẤT: Danh sách các dịch vụ trong hóa đơn này ---
        // Quan hệ 1-N (1 Hóa đơn có nhiều Dòng chi tiết)
        public virtual ICollection<BillingTransactionItem> TransactionItems { get; set; }

        public BillingTransaction()
        {
            // Luôn khởi tạo List trong Constructor để tránh lỗi NullReferenceException
            TransactionItems = new HashSet<BillingTransactionItem>();
        }
    }
}