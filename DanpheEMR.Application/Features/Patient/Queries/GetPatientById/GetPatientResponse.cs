using System;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.Patients.Queries.GetPatientById
{
    public record GetPatientResponse(
        Guid Id,
        string PatientCode,
        string FullName,
        DateTime DOB,
        string Gender,
        string PhoneNumber,
        string IdCardNumber,
        string BloodGroup,
        List<PatientAddressDto> Addresses,
        List<PatientKinDto> Kins
    );

    public record PatientAddressDto(string AddressType, string Street, string City);
    public record PatientKinDto(string FullName, string Relation, string ContactNumber);
}