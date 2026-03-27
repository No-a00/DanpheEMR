using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Billing.Queries.GetDailyRevenueReport
{
    public record GetDailyRevenueReportQuery(
        DateTime ReportDate
    ) : IRequest<Result<GetDailyRevenueReportResponse>>;
}