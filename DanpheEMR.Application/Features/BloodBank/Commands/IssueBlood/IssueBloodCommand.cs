using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.BloodBank.Commands.IssueBlood
{
    public record IssueBloodCommand(
        Guid PatientId,
        Guid BloodGroupId,
        int Quantity,
        string Remarks
    ) : IRequest<Result<bool>>;
}