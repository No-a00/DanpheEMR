using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Billing.Queries.GetProviderRevenueReport
{
    public record GetProviderRevenueReportQuery(
        string ProviderCode,
        DateTime FromDate,
        DateTime ToDate
    ) : IRequest<Result<GetProviderRevenueReportResponse>>;
}