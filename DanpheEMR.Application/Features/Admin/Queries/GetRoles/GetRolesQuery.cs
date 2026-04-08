using Application.Common;
using MediatR;


namespace DanpheEMR.Application.Features.Admin.Queries.GetRoles

    public record RoleDto(Guid Id, string RoleName, string Description);

    public record GetRolesQuery() : IRequest<Result<List<RoleDto>>>;
}