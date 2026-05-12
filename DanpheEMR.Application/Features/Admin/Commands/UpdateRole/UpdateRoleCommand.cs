using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.UpdateRole
{
    public record UpdateRoleCommand(
        string RoleName,
        string Description
    ) : IRequest<Result<bool>>;
}