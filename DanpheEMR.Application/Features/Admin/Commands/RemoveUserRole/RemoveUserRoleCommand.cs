using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.RemoveUserRole
{
    public record RemoveUserRoleCommand(
        Guid UserId,
        Guid RoleId
    ) : IRequest<Result<bool>>;
}