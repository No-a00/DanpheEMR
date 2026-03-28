
using DanpheEMR.Core.Interface.Admin;
using MediatR;


namespace DanpheEMR.Application.Features.Admin.Queries.GetPermissions
{
    public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, Result<List<PermissionDto>>>
    {
        private readonly IPermissionRepository _permissionRepository;

        public GetPermissionsQueryHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Result<List<PermissionDto>>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetAllAsync();

            var permissionDtos = permissions.Select(p => new PermissionDto(
                p.Id,
                p.Resource,
                p.Action,
                p.Description
            )).ToList();

            return Result<List<PermissionDto>>.Success(permissionDtos);
        }
    }
}