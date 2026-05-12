using Application.Common;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Base;
using DanpheEMR.Core.Interface.Billing; // Chứa IBillingTransactionRepository
using MediatR;


namespace DanpheEMR.Application.Features.Billing.Queries.GetProviderRevenueReport
{
    public class GetProviderRevenueReportQueryHandler : IRequestHandler<GetProviderRevenueReportQuery, Result<GetProviderRevenueReportResponse>>
    {
        private readonly IBillingTransactionRepository _billingRepository;
        private readonly IGenericRepository<Employee> _employeeRepository;
        public GetProviderRevenueReportQueryHandler(IBillingTransactionRepository billingRepository, IGenericRepository<Employee> employeeRepository)
        {   
            _billingRepository = billingRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<Result<GetProviderRevenueReportResponse>> Handle(GetProviderRevenueReportQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var provider = await _employeeRepository.GetFirstOrDefaultAsync(e => e.Code == request.ProviderCode);
                if (provider == null) return Result<GetProviderRevenueReportResponse>.Failure(new Error(
                    "GetProviderRevenueReport.NotFound",
                    "không tìm thấy bác sĩ với mã đã cung cấp."));

                decimal totalRevenue = await _billingRepository.CalculateTotalRevenueByProviderAsync(
                    provider.Id,
                    request.FromDate,
                    request.ToDate);

                var response = new GetProviderRevenueReportResponse
                {
                    ProviderCode = provider.Code,
                    FromDate = request.FromDate,
                    ToDate = request.ToDate,
                    TotalRevenue = totalRevenue
                };

                return Result<GetProviderRevenueReportResponse>.Success(response);
            }
            catch (Exception ex)
            {
                var error = new Error(
                    "GetProviderRevenueReport.Exception",
                    $"Đã xảy ra lỗi khi tính toán doanh thu của bác sĩ. {ex.Message}");
                return Result<GetProviderRevenueReportResponse>.Failure(error);
            }
        }
    }
}