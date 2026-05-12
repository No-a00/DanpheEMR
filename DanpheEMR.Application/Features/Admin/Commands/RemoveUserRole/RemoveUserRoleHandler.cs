using Application.Common;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.Core.Interface.Base;
using MediatR;

namespace DanpheEMR.Application.Features.Admin.Commands.RemoveUserRole
{
    public class RemoveUserRoleHandler : IRequestHandler<RemoveUserRoleCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<Role> _roleRepo;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveUserRoleHandler(IUserRepository userRepository, IUnitOfWork unitOfWork,IGenericRepository<Role> roleRepo)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _roleRepo = roleRepo;
        }

        public async Task<Result<bool>> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetFirstOrDefaultAsync(p => p.Code == request.UserCode);
                if (user == null) return Result<bool>.Failure(new Error("RemoveUserRole.NotFound", "Không tìm thấy người dùng "));
                var role = await _roleRepo.GetFirstOrDefaultAsync(p => p.RoleName == request.RoleCode);
                if (role == null) return Result<bool>.Failure(new Error("RemoveUserRole.NotFound", "Không tìm thấy Role "));

                var userRole = await _userRepository.GetUserRoleAsync(user.Id, role.Id);

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
            catch (Exception ex)
            {

                return Result<bool>.Failure(new Error("RemoveUserRole.Exception", $"{ex.Message}"));
            }
        }
    }
}