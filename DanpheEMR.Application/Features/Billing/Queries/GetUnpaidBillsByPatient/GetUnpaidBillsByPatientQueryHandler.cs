using Application.Common;
using DanpheEMR.Core.Interface.Billing;
using MediatR;

namespace DanpheEMR.Application.Features.Billing.Queries.GetUnpaidBillsByPatient
{
    public class GetUnpaidBillsByPatientQueryHandler : IRequestHandler<GetUnpaidBillsByPatientQuery, Result<GetUnpaidBillsByPatientResponse>>
    {
        private readonly IBillingTransactionRepository _billingRepository;

        public GetUnpaidBillsByPatientQueryHandler(IBillingTransactionRepository billingRepository)
        {
            _billingRepository = billingRepository;
        }

        public async Task<Result<GetUnpaidBillsByPatientResponse>> Handle(GetUnpaidBillsByPatientQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var unpaidTransactions = await _billingRepository.GetUnpaidTransactionsByPatientAsync(request.PatientId);

                var dtos = unpaidTransactions.ToDtoList();

                var response = new GetUnpaidBillsByPatientResponse
                {
                    PatientId = request.PatientId,
                    TotalUnpaidBills = dtos.Count,
                    TotalUnpaidAmount = dtos.Sum(x => x.TotalAmount),
                    UnpaidBills = dtos
                };

                return Result<GetUnpaidBillsByPatientResponse>.Success(response);
            }
            catch (Exception)
            {
                var error = new Error("GetUnpaidBills.Error", "Đã xảy ra lỗi khi tải danh sách hóa đơn nợ.");
                return Result<GetUnpaidBillsByPatientResponse>.Failure(error);
            }
        }
    }
}