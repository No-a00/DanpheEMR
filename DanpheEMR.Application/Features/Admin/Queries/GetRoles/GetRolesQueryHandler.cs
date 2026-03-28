using Application.Common;
using DanpheEMR.Core.Interface.Admin;
using MediatR;
namespace DanpheEMR.Application.Features.Admin.Queries.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, Result<List<RoleDto>>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRolesQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Result<List<RoleDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAllAsync();

            var roleDtos = roles.Select(r => new RoleDto(
                r.Id,
                r.RoleName,
                r.Description
            )).ToList();

            return Result<List<RoleDto>>.Success(roleDtos);
        }
    }
}