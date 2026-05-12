
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.Core.Interface.Base;
using Application.Common;
using MediatR;


namespace DanpheEMR.Application.Features.Admin.Commands.AssignRolePermission
{
    public class AssignRolePermissionHandler : IRequestHandler<AssignRolePermissionCommand, Result<bool>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IGenericRepository<Role> _roleRepo;
        private readonly IUnitOfWork _unitOfWork;

        public AssignRolePermissionHandler(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork,IGenericRepository<Role> roleRepo)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
            _roleRepo = roleRepo;     
     
        }

        public async Task<Result<bool>> Handle(AssignRolePermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await  _roleRepo.GetFirstOrDefaultAsync(p => p.RoleName == request.RoleCode);
                if (role == null) return Result<bool>.Failure(new Error("Role.NotFound", "không tìm thấy Role"));
                var permission = await _permissionRepository.GetFirstOrDefaultAsync(p => p.PerCode == request.PermissionCode);
                if (permission == null) return Result<bool>.Failure(new Error("Permission.NotFound", "không tìm thây Permission"));

                bool hasPermission = await _permissionRepository.RoleHasPermissionAsync(role.Id, permission.Id);
                if (hasPermission)
                {
                    return Result<bool>.Failure(AssignRolePermissionErrors.PermissionAlreadyAssigned);
                }

                var rolePermission = new RolePermission
                {
                    Id = Guid.NewGuid(),
                    RoleId = role.Id,
                    PermissionId = permission.Id,
                    IsActive = true
                };

                await _permissionRepository.AddRolePermissionAsync(rolePermission);
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                return saveResult > 0
                    ? Result<bool>.Success(true)
                    : Result<bool>.Failure(AssignRolePermissionErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<bool>.Failure(AssignRolePermissionErrors.DatabaseError);
            }
        }
    }
}