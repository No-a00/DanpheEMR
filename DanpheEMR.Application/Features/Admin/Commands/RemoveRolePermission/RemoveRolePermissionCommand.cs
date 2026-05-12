using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.RemoveRolePermission
{
    public record RemoveRolePermissionCommand(
        string RoleCode,
        string PermissionCode
    ) : IRequest<Result<bool>>;
}