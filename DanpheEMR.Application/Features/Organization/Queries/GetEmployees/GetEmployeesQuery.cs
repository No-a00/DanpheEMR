using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.Admin.Queries.GetEmployees
{
    public record GetEmployeesQuery(
        string SearchTerm = null, 
        Guid? DepartmentId = null 
    ) : IRequest<Result<List<GetEmployeesResponse>>>;
}