using Application.Common;
using DanpheEMR.Core.Interface.Billing; // Chứa IBillingTransactionRepository
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.Billing.Queries.GetDailyRevenueReport
{
    // 1. Đã đổi thành public và thêm chữ 'r'
    public class GetDailyRevenueReportQueryHandler : IRequestHandler<GetDailyRevenueReportQuery, Result<GetDailyRevenueReportResponse>>
    {
        private readonly IBillingTransactionRepository _billingRepository;

        public GetDailyRevenueReportQueryHandler(IBillingTransactionRepository billingRepository)
        {
            _billingRepository = billingRepository;
        }

        public async Task<Result<GetDailyRevenueReportResponse>> Handle(GetDailyRevenueReportQuery request, CancellationToken cancellationToken)
        {
            try
            {
               
                var transactions = await _billingRepository.GetPaidTransactionsByDateAsync(request.ReportDate.Date);

              
                var transactionDtos = transactions.ToDtoList();

                
                var response = new GetDailyRevenueReportResponse
                {
                    ReportDate = request.ReportDate.Date,
                    TotalTransactions = transactionDtos.Count,
                   
                    TotalRevenue = transactionDtos.Sum(t => t.Amount),
                    Transactions = transactionDtos
                };

                return Result<GetDailyRevenueReportResponse>.Success(response);
            }
            catch (Exception)
            {
                var error = new Error("GetDailyRevenueReport.Error", "Đã xảy ra lỗi khi lập báo cáo doanh thu trong ngày.");
                return Result<GetDailyRevenueReportResponse>.Failure(error);
            }
        }
    }
}