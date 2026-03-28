using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Patients.Commands.TransferPatient
{
    public record TransferPatientCommand(
        Guid AdmissionId,
        Guid FromDeptId,
        Guid ToDeptId,
        string Reason
    ) : IRequest<Result<Guid>>; 
}