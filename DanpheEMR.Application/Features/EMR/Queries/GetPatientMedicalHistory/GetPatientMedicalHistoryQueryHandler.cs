using Application.Common;
using DanpheEMR.Core.Interface.EMR; 
using MediatR;


namespace DanpheEMR.Application.Features.EMR.Queries.GetPatientMedicalHistory
{
    public class GetPatientMedicalHistoryQueryHandler : IRequestHandler<GetPatientMedicalHistoryQuery, Result<GetPatientMedicalHistoryResponse>>
    {
        private readonly IDiagnosisRepository _diagnosisRepository;

        public GetPatientMedicalHistoryQueryHandler(IDiagnosisRepository diagnosisRepository)
        {
            _diagnosisRepository = diagnosisRepository;
        }

        public async Task<Result<GetPatientMedicalHistoryResponse>> Handle(GetPatientMedicalHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                
                var diagnoses = await _diagnosisRepository.GetDiagnosesByPatientIdAsync(request.PatientId);

                
                var historyDtos = diagnoses
                    .Where(d => !d.IsDeleted)
                    .OrderByDescending(d => d.DiagnosisDate)
                    .ToDtoList();

                var response = new GetPatientMedicalHistoryResponse
                {
                    PatientId = request.PatientId,
                    TotalRecords = historyDtos.Count,
                    History = historyDtos
                };

                return Result<GetPatientMedicalHistoryResponse>.Success(response);
            }
            catch (Exception)
            {
                return Result<GetPatientMedicalHistoryResponse>.Failure(
                    new Error("GetMedicalHistory.Error", "Đã xảy ra lỗi khi tải hồ sơ bệnh án.")
                );
            }
        }
    }
}