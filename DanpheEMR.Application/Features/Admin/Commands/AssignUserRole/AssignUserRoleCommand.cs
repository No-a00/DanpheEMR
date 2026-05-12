using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.AssignUserRole
{
    public record AssignUserRoleCommand(
        string UserCode,  
        string RoleCode   
    ) : IRequest<Result<bool>>;
}