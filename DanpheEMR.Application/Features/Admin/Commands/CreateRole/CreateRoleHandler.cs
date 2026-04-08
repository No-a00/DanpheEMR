
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin; 
using MediatR;


namespace DanpheEMR.Application.Features.Admin.Commands.CreateRole
{
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, Result<Guid>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {

                bool isExists = await _roleRepository.IsRoleNameExistsAsync(request.RoleName);
                if (isExists)
                {
                    return Result<Guid>.Failure(CreateRoleErrors.RoleNameExists);
                }

                // 2. Map dữ liệu sang Entity
                var role = new Role
                {
                    Id = Guid.NewGuid(),
                    RoleName = request.RoleName,
                    Description = request.Description,
                };

                await _roleRepository.AddAsync(role);

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (saveResult > 0)
                {
                    return Result<Guid>.Success(role.Id);
                }

                return Result<Guid>.Failure(CreateRoleErrors.DatabaseError);
            }
            catch (Exception)
            {
                return Result<Guid>.Failure(CreateRoleErrors.DatabaseError);
            }
        }
    }
}