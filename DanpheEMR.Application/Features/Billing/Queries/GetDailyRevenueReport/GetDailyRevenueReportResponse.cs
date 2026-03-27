
namespace DanpheEMR.Application.Features.Billing.Queries.GetDailyRevenueReport
{
    public class GetDailyRevenueReportResponse
    {
        public DateTime ReportDate { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalTransactions { get; set; }

        public List<RevenueTransactionItemDto> Transactions { get; set; } = new();
    }

    public class RevenueTransactionItemDto
    {
        public Guid TransactionId { get; set; }
        public string InvoiceNumber { get; set; }

        public TimeSpan TransactionTime { get; set; }
        public string PatientName { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}