using Application.Common;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Admin;
using MediatR;

namespace DanpheEMR.Application.Features.Admin.Commands.RemoveUserRole
{
    public class RemoveUserRoleHandler : IRequestHandler<RemoveUserRoleCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveUserRoleHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userRole = await _userRepository.GetUserRoleAsync(request.UserId, request.RoleId);

            if (userRole == null)
            {
                return Result<bool>.Failure(new Error("RemoveUserRole.NotFound", "Người dùng này không sở hữu vai trò này."));
            }

            _userRepository.RemoveUserRole(userRole);
            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return saveResult > 0
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(new Error("RemoveUserRole.DatabaseError", "Lỗi khi gỡ vai trò khỏi người dùng."));
        }
    }
}