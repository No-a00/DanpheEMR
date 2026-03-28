using Application.Common;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Admin;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.Admin.Commands.AssignUserRole
{
    public class AssignUserRoleHandler : IRequestHandler<AssignUserRoleCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignUserRoleHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);
                if (user == null)
                {
                    return Result<bool>.Failure(AssignUserRoleErrors.UserNotFound);
                }

                bool hasRole = await _userRepository.UserHasRoleAsync(request.UserId, request.RoleId);
                if (hasRole)
                {
                    return Result<bool>.Failure(AssignUserRoleErrors.RoleAlreadyAssigned);
                }

                var userRole = new UserRole
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    RoleId = request.RoleId,
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