
using DanpheEMR.Core.Interface.Patients;
using MediatR;

namespace DanpheEMR.Application.Features.Patients.Queries.GetAdmittedPatients
{
    public class GetAdmittedPatientsQueryHandler : IRequestHandler<GetAdmittedPatientsQuery, Result<List<GetAdmittedPatientsResponse>>>
    {
        private readonly IAdmissionRepository _admissionRepository;

        public GetAdmittedPatientsQueryHandler(IAdmissionRepository admissionRepository)
        {
            _admissionRepository = admissionRepository;
        }

        public async Task<Result<List<GetAdmittedPatientsResponse>>> Handle(GetAdmittedPatientsQuery request, CancellationToken cancellationToken)
        {
            var admissions = await _admissionRepository.GetActiveAdmissionsAsync();

            var result = admissions.Select(a => new GetAdmittedPatientsResponse(
                a.Id,
                a.Patient.PatientCode,
                a.Patient.FullName,
                a.AdmissionDate,
                a.AdmittingDoctor.FullName,
                a.Status.ToString()
            )).ToList();

            return Result<List<GetAdmittedPatientsResponse>>.Success(result);
        }
    }
}