using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;
using System.ComponentModel.DataAnnotations;

namespace DanpheEMR.Core.Domain.Billing
{
    public class BillingTransaction : BaseEntity,ISoftDelete
    {
        [Key]
        public Guid Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        [Required]
        public PaymentMode PaymentMode { get; set; }
        [Required]
        public TransactionType TransactionType { get; set; }
        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }
        public Guid? DeletedBy { get; set; }
        //
        public decimal SubTotal { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public PaymentStatus PaymentStatus { get; set; } 
        public string Remarks { get; set; }

       
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

        public Guid VisitId { get; set; }
        public Visit Visit { get; set; }

        public virtual ICollection<BillingTransactionItem> TransactionItems { get; set; }

        public BillingTransaction()
        {
            TransactionItems = new HashSet<BillingTransactionItem>();
        }
    }
}