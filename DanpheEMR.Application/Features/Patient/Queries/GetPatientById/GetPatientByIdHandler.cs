
using DanpheEMR.Core.Interface.Patients;
using MediatR;

namespace DanpheEMR.Application.Features.Patients.Queries.GetPatientById
{
    public class GetPatientByIdHandler : IRequestHandler<GetPatientByIdQuery, Result<GetPatientResponse>>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientByIdHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Result<GetPatientResponse>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var p = await _patientRepository.GetPatientWithDetailsAsync(request.Id);

            if (p == null) return Result<GetPatientResponse>.Failure(GetPatientByIdErrors.NotFound);

            var response = new GetPatientResponse(
                p.Id, p.PatientCode, p.FullName, p.DOB, p.Gender, p.PhoneNumber, p.IdCardNumber, p.BloodGroup,
                p.Addresses.Select(a => new PatientAddressDto(a.AddressType, a.Street, a.City)).ToList(),
                p.Kins.Select(k => new PatientKinDto(k.FullName, k.Relation, k.ContactNumber)).ToList()
            );

            return Result<GetPatientResponse>.Success(response);
        }
    }
}