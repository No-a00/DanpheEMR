using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.RegisterEmployee
{
    public record RegisterEmployeeCommand(
        string FirstName,
        string LastName,
        string Workforce,
        string DepartmentCode,
        DateTime DOB,
        string Gender,
        string ContactNumber
        
    ) : IRequest<Result<Guid>>; 
}