using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Billing
{
    public class Receipt : BaseEntity,IHasActiveStatus
    {
        public Guid Id { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime ReceiptDate { get; set; }
        //hủy và lí do
        public bool IsActive { get; set; }
        public string CancelReason { get; set; }
        public int? CancelUserId { get; set; }
        //
        public decimal AmountPaid { get; set; }
        public PaymentStatus PaymentMode { get; set; }
        public int BillingtransactionId { get; set; }
        public BillingTransaction BillingTransaction { get; set; }
    }
}
