using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.RemoveUserRole
{
    public record RemoveUserRoleCommand(
        string UserCode,
        string RoleCode
    ) : IRequest<Result<bool>>;
}