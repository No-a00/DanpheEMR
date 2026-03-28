using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Patients.Commands.RegisterPatient
{
    public record RegisterPatientCommand(
        string FirstName,
        string LastName,
        DateTime DOB,
        string Gender,
        string PhoneNumber,
        string IdCardNumber, 
        string BloodGroup
    ) : IRequest<Result<RegisterPatientResponse>>;
}