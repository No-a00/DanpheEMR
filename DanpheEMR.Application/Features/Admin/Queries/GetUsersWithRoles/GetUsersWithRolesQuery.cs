using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.Admin.Queries.GetUsersWithRoles
{
    public record UserWithRolesDto(
        Guid Id,
        string FullName,
        string Email,
        List<string> Roles 
    );

    public record GetUsersWithRolesQuery() : IRequest<Result<List<UserWithRolesDto>>>;
}