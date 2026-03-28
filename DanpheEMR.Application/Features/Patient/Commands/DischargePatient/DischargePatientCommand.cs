using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Patients.Commands.DischargePatient
{
    public record DischargePatientCommand(
        Guid AdmissionId,
        string DischargeCondition,
        string DischargeNotes      
    ) : IRequest<Result<bool>>;
}