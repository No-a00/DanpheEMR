using Application.Common;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Admin;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.Admin.Commands.DeleteRole
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, Result<bool>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoleHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id);
            if (role == null)
            {
                return Result<bool>.Failure(new Error("DeleteRole.NotFound", "Không tìm thấy Vai trò này."));
            }

            await _roleRepository.DeleteAsync(role.Id); 
            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return saveResult > 0
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(new Error("DeleteRole.DatabaseError", "Lỗi khi xóa vai trò."));
        }
    }
}