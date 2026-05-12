using Application.Common;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Base;
using DanpheEMR.Core.Interface.Billing;
using MediatR;

namespace DanpheEMR.Application.Features.Billing.Queries.GetUnpaidBillsByPatient
{
    public class GetUnpaidBillsByPatientQueryHandler : IRequestHandler<GetUnpaidBillsByPatientQuery, Result<GetUnpaidBillsByPatientResponse>>
    {
        private readonly IBillingTransactionRepository _billingRepository;
        private readonly IGenericRepository<Patient> _patientRepo;
        public GetUnpaidBillsByPatientQueryHandler(IBillingTransactionRepository billingRepository, IGenericRepository<Patient> patientRepo)
        {
            _billingRepository = billingRepository;
            _patientRepo = patientRepo;
        }

        public async Task<Result<GetUnpaidBillsByPatientResponse>> Handle(GetUnpaidBillsByPatientQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var patient = await _patientRepo.GetFirstOrDefaultAsync(p=>p.PatientCode==request.PatientCode);
                if(patient == null) return Result<GetUnpaidBillsByPatientResponse>.Failure(new Error("GetUnpaidBills.PatientNotFound", $"Không tìm thấy bệnh nhân với mã {request.PatientCode}."));

                var unpaidTransactions = await _billingRepository.GetUnpaidTransactionsByPatientAsync(patient.Id);

                var dtos = unpaidTransactions.ToDtoList();

                var response = new GetUnpaidBillsByPatientResponse
                {
                    PatientCode = request.PatientCode,
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