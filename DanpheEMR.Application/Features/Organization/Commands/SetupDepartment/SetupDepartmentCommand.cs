using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Admin.Commands.SetupDepartment
{
    public record SetupDepartmentCommand(
        string DepartmentCode,
        string DepartmentName,
        bool IsClinical,
        bool IsActive,
        Guid? ParentDepartmentId,  
        Guid? HeadOfDepartmentId  
    ) : IRequest<Result<Guid>>;
}