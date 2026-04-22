using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Billing
{
    public class Receipt : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime ReceiptDate { get; set; }

        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }
        public Guid? DeletedBy { get; set; }

        public decimal AmountPaid { get; set; }
        public PaymentStatus PaymentMode { get; set; }

        public Guid BillingtransactionId { get; set; } 
        public BillingTransaction BillingTransaction { get; set; }
    }
}