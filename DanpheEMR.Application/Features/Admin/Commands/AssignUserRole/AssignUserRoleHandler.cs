
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.Core.Interface.Base;
using MediatR;


namespace DanpheEMR.Application.Features.Admin.Commands.AssignUserRole
{
    public class AssignUserRoleHandler : IRequestHandler<AssignUserRoleCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Role> _roleRepo;

        public AssignUserRoleHandler(IUserRepository userRepository, IUnitOfWork unitOfWork,IGenericRepository<Role> roleRepo)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _roleRepo = roleRepo;
        }

        public async Task<Result<bool>> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetFirstOrDefaultAsync(p=>p.Code == request.UserCode);
                if (user == null)
                {
                    return Result<bool>.Failure(AssignUserRoleErrors.UserNotFound);
                }
                var role = await _roleRepo.GetFirstOrDefaultAsync(p => p.RoleName == request.RoleCode);
                if (role == null) return Result<bool>.Failure(AssignUserRoleErrors.RoleNotFound);

                bool hasRole = await _userRepository.UserHasRoleAsync(user.Id, role.Id);
                if (hasRole)
                {
                    return Result<bool>.Failure(AssignUserRoleErrors.RoleAlreadyAssigned);
                }

                var userRole = new UserRole
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    RoleId = role.Id,
                    IsActive = true
                };

                await _userRepository.AddUserRoleAsync(userRole);
                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    return Result<bool>.Success(true);
                }

                return Result<bool>.Failure(AssignUserRoleErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<bool>.Failure(AssignUserRoleErrors.DatabaseError);
            }
        }
    }
}