using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.DeleteRole
{
    public record DeleteRoleCommand(string RoleName) : IRequest<Result<bool>>;
}