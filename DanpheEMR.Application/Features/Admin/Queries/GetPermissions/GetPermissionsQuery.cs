using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.Admin.Queries.GetPermissions
{
    public record PermissionDto(Guid Id, string Resource, string Action, string Description);

    public record GetPermissionsQuery() : IRequest<Result<List<PermissionDto>>>;
}