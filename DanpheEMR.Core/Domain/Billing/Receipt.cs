using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Nums;

namespace DanpheEMR.Core.Domain.Billing
{
    public class Receipt : BaseEntity
    {
        public int Id { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime ReceiptDate { get; set; }
        public decimal AmountPaid { get; set; }
        public PaymentStatus  PaymentMode { get; set; }
        public int BillingtransactionId { get; set; }
        public BillingTransaction BillingTransaction { get; set; }
    }
}
