using Application.Common;
using DanpheEMR.Application.Features.Admin.Queries.GetPermissions;
using DanpheEMR.Core.Interface.Admin;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.Admin.Queries.GetRoleDetails
{
    public class GetRoleDetailsQueryHandler : IRequestHandler<GetRoleDetailsQuery, Result<RoleDetailsDto>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRoleDetailsQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Result<RoleDetailsDto>> Handle(GetRoleDetailsQuery request, CancellationToken cancellationToken)
        {
          
            var role = await _roleRepository.GetRoleWithPermissionsAsync(request.Id);

            if (role == null)
            {
                return Result<RoleDetailsDto>.Failure(new Error("GetRoleDetails.NotFound", "Không tìm thấy Vai trò này."));
            }

            var roleDetails = new RoleDetailsDto(
                role.Id,
                role.RoleName,
                role.Description,
                role.RolePermissions.Select(rp => new PermissionDto(
                    rp.Permission.Id,
                    rp.Permission.Resource,
                    rp.Permission.Action,
                    rp.Permission.Description
                )).ToList()
            );

            return Result<RoleDetailsDto>.Success(roleDetails);
        }
    }
}