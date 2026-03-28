using Application.Common;
using MediatR;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.Admin.Queries.GetDepartmentTree
{
    public record GetDepartmentTreeQuery() : IRequest<Result<List<GetDepartmentTreeResponse>>>;
}