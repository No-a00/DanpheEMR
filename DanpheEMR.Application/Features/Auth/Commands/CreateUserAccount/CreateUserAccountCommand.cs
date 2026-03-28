using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Auth.Commands.CreateUserAccount
{
    public record CreateUserAccountCommand(
        string UserName,
        string Email,
        string Password, 
        Guid? EmployeeId 
    ) : IRequest<Result<Guid>>;
}