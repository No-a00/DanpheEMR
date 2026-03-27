using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Billing.Queries.GetProviderRevenueReport
{
    public record GetProviderRevenueReportQuery(
        Guid ProviderId,
        DateTime FromDate,
        DateTime ToDate
    ) : IRequest<Result<GetProviderRevenueReportResponse>>;
}