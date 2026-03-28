
using DanpheEMR.Core.Interface.Patients;
using MediatR;


namespace DanpheEMR.Application.Features.Patients.Queries.GetPatientHistory
{
    public class GetPatientHistoryHandler : IRequestHandler<GetPatientHistoryQuery, Result<PatientHistoryResponse>>
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IAdmissionRepository _admissionRepository;

        public GetPatientHistoryHandler(IVisitRepository visitRepository, IAdmissionRepository admissionRepository)
        {
            _visitRepository = visitRepository;
            _admissionRepository = admissionRepository;
        }

        public async Task<Result<PatientHistoryResponse>> Handle(GetPatientHistoryQuery request, CancellationToken cancellationToken)
        {
            var visits = await _visitRepository.GetVisitsByPatientIdAsync(request.PatientId);
            var admissions = await _admissionRepository.GetAdmissionsByPatientIdAsync(request.PatientId);

            var response = new PatientHistoryResponse(
                visits.Select(v => new VisitHistoryDto(v.Id, v.VisitDate, v.Department.DepartmentName, v.Provider.FullName, v.Status.ToString())).ToList(),
                admissions.Select(a => new AdmissionHistoryDto(a.Id, a.AdmissionDate, a.Discharge?.DischargeDate, a.Status.ToString())).ToList()
            );

            return Result<PatientHistoryResponse>.Success(response);
        }
    }
}