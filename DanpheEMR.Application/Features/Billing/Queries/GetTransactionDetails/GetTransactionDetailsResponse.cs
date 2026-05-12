using DanpheEMR.Core.Enums;

namespace DanpheEMR.Application.Features.Billing.Queries.GetTransactionDetails
{
    public class GetTransactionDetailsResponse
    {
        public string TransactionCode { get; set; }
        public string ReceiptNumber { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; } 
        public PaymentMode PaymentMethod { get; set; } 
        public List<TransactionItemDto> Items { get; set; } = new();
    }

    public class TransactionItemDto
    {
        public Guid ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
    }
}