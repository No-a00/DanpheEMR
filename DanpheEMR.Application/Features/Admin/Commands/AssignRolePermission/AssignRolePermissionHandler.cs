
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Admin; 
using MediatR;


namespace DanpheEMR.Application.Features.Admin.Commands.AssignRolePermission
{
    public class AssignRolePermissionHandler : IRequestHandler<AssignRolePermissionCommand, Result<bool>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignRolePermissionHandler(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(AssignRolePermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool hasPermission = await _permissionRepository.RoleHasPermissionAsync(request.RoleId, request.PermissionId);
                if (hasPermission)
                {
                    return Result<bool>.Failure(AssignRolePermissionErrors.PermissionAlreadyAssigned);
                }

                var rolePermission = new RolePermission
                {
                    Id = Guid.NewGuid(),
                    RoleId = request.RoleId,
                    PermissionId = request.PermissionId,
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