using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.AssignRolePermission
{
    public record AssignRolePermissionCommand(
        Guid RoleId,
        Guid PermissionId
    ) : IRequest<Result<bool>>;
}