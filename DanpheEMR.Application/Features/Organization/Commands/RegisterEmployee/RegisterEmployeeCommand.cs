using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.RegisterEmployee
{
    public record RegisterEmployeeCommand(
        string FirstName,
        string LastName,
        DateTime DOB,
        string Gender,
        string ContactNumber,
        Guid DepartmentId
    ) : IRequest<Result<Guid>>; 
}