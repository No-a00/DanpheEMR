using System;

namespace DanpheEMR.Application.Features.Billing.Queries.GetProviderRevenueReport
{
    public class GetProviderRevenueReportResponse
    {
        public Guid ProviderId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}