using Application.Common;
using DanpheEMR.Core.Interface.Patients;
using MediatR;


namespace DanpheEMR.Application.Features.Patients.Queries.SearchPatients
{
    public class SearchPatientsQueryHandler : IRequestHandler<SearchPatientsQuery, Result<List<SearchPatientsResponse>>>
    {
        private readonly IPatientRepository _patientRepository;

        public SearchPatientsQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Result<List<SearchPatientsResponse>>> Handle(SearchPatientsQuery request, CancellationToken cancellationToken)
        {

            var patients = await _patientRepository.SearchPatientsAsync(request.SearchTerm);

            var result = patients.Select(p => new SearchPatientsResponse(
                p.Id, p.PatientCode, p.FullName, p.Gender, p.DOB, p.PhoneNumber, p.IdCardNumber
            )).ToList();

            return Result<List<SearchPatientsResponse>>.Success(result);
        }
    }
}