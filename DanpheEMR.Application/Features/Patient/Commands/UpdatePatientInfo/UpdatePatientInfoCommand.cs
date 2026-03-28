using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Patients.Commands.UpdatePatientInfo
{
    public record UpdatePatientInfoCommand(
        Guid PatientId,
        string FirstName,
        string LastName,
        DateTime DOB,
        string Gender,
        string PhoneNumber,
        string BloodGroup
    ) : IRequest<Result<bool>>;
}