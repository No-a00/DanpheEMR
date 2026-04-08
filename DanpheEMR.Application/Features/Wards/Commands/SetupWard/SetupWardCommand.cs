using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Inpatient.Commands.SetupWard
{
    public record SetupWardCommand(
        string WardName,
        string WardCode,
        Guid DepartmentId
    ) : IRequest<Result<Guid>>;
}