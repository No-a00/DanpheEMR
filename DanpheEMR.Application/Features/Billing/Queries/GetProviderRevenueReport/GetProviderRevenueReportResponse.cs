using System;

namespace DanpheEMR.Application.Features.Billing.Queries.GetProviderRevenueReport
{
    public class GetProviderRevenueReportResponse
    {
        public string ProviderCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}