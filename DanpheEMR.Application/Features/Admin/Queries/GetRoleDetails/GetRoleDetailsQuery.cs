
using MediatR;

using DanpheEMR.Application.Features.Admin.Queries.GetPermissions; 
namespace DanpheEMR.Application.Features.Admin.Queries.GetRoleDetails
{
    public record RoleDetailsDto(
        Guid Id,
        string RoleName,
        string Description,
        List<PermissionDto> Permissions
    );

    public record GetRoleDetailsQuery(Guid Id) : IRequest<Result<RoleDetailsDto>>;
}