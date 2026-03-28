using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.RemoveRolePermission
{
    public record RemoveRolePermissionCommand(
        Guid RoleId,
        Guid PermissionId
    ) : IRequest<Result<bool>>;
}