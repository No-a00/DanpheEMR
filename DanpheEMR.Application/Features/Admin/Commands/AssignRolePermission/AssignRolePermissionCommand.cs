using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.AssignRolePermission
{
    public record AssignRolePermissionCommand(
        string RoleCode,
        string PermissionCode
    ) : IRequest<Result<bool>>;
}