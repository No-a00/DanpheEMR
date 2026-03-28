using System;

namespace DanpheEMR.Application.Features.Patients.Queries.SearchPatients
{
    public record SearchPatientsResponse(
        Guid Id,
        string PatientCode,
        string FullName,
        string Gender,
        DateTime DOB,
        string PhoneNumber,
        string IdCardNumber
    );
}