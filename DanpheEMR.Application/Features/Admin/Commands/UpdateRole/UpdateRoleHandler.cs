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

            try
            {
                var role = await _roleRepository.GetFirstOrDefaultAsync(p => p.RoleName == request.RoleName);
                if (role == null)
                {
                    return Result<bool>.Failure(new Error("UpdateRole.NotFound", "Không tìm thấy Vai trò này."));
                }
                role.RoleName = request.RoleName;
                role.Description = request.Description;

                _roleRepository.Update(role);
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                return saveResult > 0
                    ? Result<bool>.Success(true)
                    : Result<bool>.Failure(new Error("UpdateRole.DatabaseError", "Lỗi khi lưu cập nhật."));
            }
            catch (Exception ex)
            {

                return Result<bool>.Failure(new Error("UpdateRole.Exception", $"{ex.Message}"));
            }
        }
    }
}