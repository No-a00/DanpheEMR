
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

    public record GetRoleDetailsQuery(string RoleCode) : IRequest<Result<RoleDetailsDto>>;
}