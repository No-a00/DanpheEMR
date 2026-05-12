using Application.Common;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.Core.Interface.Base;
using MediatR;
namespace DanpheEMR.Application.Features.Admin.Commands.RemoveRolePermission
{
    public class RemoveRolePermissionHandler : IRequestHandler<RemoveRolePermissionCommand, Result<bool>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IGenericRepository<Role> _roleRepo;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRolePermissionHandler(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork,IGenericRepository<Role> roleRepo)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
            _roleRepo = roleRepo;
        }

        public async Task<Result<bool>> Handle(RemoveRolePermissionCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var role = await _roleRepo.GetFirstOrDefaultAsync(p => p.RoleName == request.RoleCode);
                if (role == null) return Result<bool>.Failure(new Error("RemoveRolePermission.NotFound", "không tìm thấy Role"));
                var permission = await _permissionRepository.GetFirstOrDefaultAsync(p => p.PerCode == request.PermissionCode);
                if (permission == null) return Result<bool>.Failure(new Error("RemoveRolePermission.NotFound", "không tìm thấy Permission"));

                var rolePermission = await _permissionRepository.GetRolePermissionAsync(role.Id, permission.Id);

                if (rolePermission == null)
                {
                    return Result<bool>.Failure(new Error("RemoveRolePermission.NotFound", "Vai trò này chưa được gán quyền này."));
                }

                _permissionRepository.RemoveRolePermission(rolePermission);
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                return saveResult > 0
                    ? Result<bool>.Success(true)
                    : Result<bool>.Failure(new Error("RemoveRolePermission.DatabaseError", "Lỗi khi gỡ quyền."));
            }
            catch (Exception ex)
            {

                return Result<bool>.Failure(new Error("RemoveRolePermission.Exception", $"{ex.Message}"));
            }
        }
    }
}