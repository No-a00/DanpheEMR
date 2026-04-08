using Application.Common;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Interface.Admin;
using MediatR;


namespace DanpheEMR.Application.Features.Admin.Commands.UpdateRole
{
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, Result<bool>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoleHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id);
            if (role == null)
            {
                return Result<bool>.Failure(new Error("UpdateRole.NotFound", "Không tìm thấy Vai trò này."));
            }
            bool isExists = await _roleRepository.IsRoleNameExistsAsync(request.RoleName, request.Id);
            if (isExists)
            {
                return Result<bool>.Failure(new Error("UpdateRole.NameExists", "Tên vai trò này đã tồn tại."));
            }

            role.RoleName = request.RoleName;
            role.Description = request.Description;

            _roleRepository.Update(role);
            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return saveResult > 0
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(new Error("UpdateRole.DatabaseError", "Lỗi khi lưu cập nhật."));
        }
    }
}