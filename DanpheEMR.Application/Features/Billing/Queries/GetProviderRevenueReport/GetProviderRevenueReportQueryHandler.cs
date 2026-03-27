using Application.Common;
using DanpheEMR.Core.Interface.Billing; // Chứa IBillingTransactionRepository
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.Billing.Queries.GetProviderRevenueReport
{
    public class GetProviderRevenueReportQueryHandler : IRequestHandler<GetProviderRevenueReportQuery, Result<GetProviderRevenueReportResponse>>
    {
        private readonly IBillingTransactionRepository _billingRepository;

        public GetProviderRevenueReportQueryHandler(IBillingTransactionRepository billingRepository)
        {
            _billingRepository = billingRepository;
        }

        public async Task<Result<GetProviderRevenueReportResponse>> Handle(GetProviderRevenueReportQuery request, CancellationToken cancellationToken)
        {
            try
            {
                decimal totalRevenue = await _billingRepository.CalculateTotalRevenueByProviderAsync(
                    request.ProviderId,
                    request.FromDate,
                    request.ToDate);

                var response = new GetProviderRevenueReportResponse
                {
                    ProviderId = request.ProviderId,
                    FromDate = request.FromDate,
                    ToDate = request.ToDate,
                    TotalRevenue = totalRevenue
                };

                return Result<GetProviderRevenueReportResponse>.Success(response);
            }
            catch (Exception)
            {
                var error = new Error(
                    "GetProviderRevenueReport.Error",
                    "Đã xảy ra lỗi khi tính toán doanh thu của bác sĩ.");
                return Result<GetProviderRevenueReportResponse>.Failure(error);
            }
        }
    }
}