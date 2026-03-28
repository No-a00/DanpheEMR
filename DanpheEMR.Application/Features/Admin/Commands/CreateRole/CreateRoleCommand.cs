using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.CreateRole
{
    public record CreateRoleCommand(
        string RoleName,
        string Description
    ) : IRequest<Result<Guid>>;
}