using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.AssignUserRole
{
    public record AssignUserRoleCommand(
        Guid UserId,  
        Guid RoleId   
    ) : IRequest<Result<bool>>;
}