using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Domain.Base;
using System; // Nhớ thêm System để dùng Guid

namespace DanpheEMR.Core.Domain.Billing
{
    public class Receipt : BaseEntity, IHasActiveStatus
    {
        public Guid Id { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime ReceiptDate { get; set; }

        public bool IsActive { get; set; }
        public string CancelReason { get; set; }
        public Guid? CancelUserId { get; set; } 

        public decimal AmountPaid { get; set; }
        public PaymentStatus PaymentMode { get; set; }

        public Guid BillingtransactionId { get; set; } 
        public BillingTransaction BillingTransaction { get; set; }
    }
}