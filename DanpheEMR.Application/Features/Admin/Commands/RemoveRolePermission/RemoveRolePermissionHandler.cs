using Application.Common;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Admin;
using MediatR;
namespace DanpheEMR.Application.Features.Admin.Commands.RemoveRolePermission
{
    public class RemoveRolePermissionHandler : IRequestHandler<RemoveRolePermissionCommand, Result<bool>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRolePermissionHandler(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(RemoveRolePermissionCommand request, CancellationToken cancellationToken)
        {
            var rolePermission = await _permissionRepository.GetRolePermissionAsync(request.RoleId, request.PermissionId);

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
    }
}