using Application.Common;
using MediatR;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.Admin.Queries.GetSystemParams
{
    public record GetSystemParamsQuery(string SearchTerm = null) : IRequest<Result<List<GetSystemParamsResponse>>>;
}