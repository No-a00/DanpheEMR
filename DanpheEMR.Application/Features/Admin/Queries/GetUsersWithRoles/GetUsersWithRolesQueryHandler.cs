
using DanpheEMR.Core.Interface.Admin;
using MediatR;


namespace DanpheEMR.Application.Features.Admin.Queries.GetUsersWithRoles
{
    public class GetUsersWithRolesQueryHandler : IRequestHandler<GetUsersWithRolesQuery, Result<List<UserWithRolesDto>>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersWithRolesQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<List<UserWithRolesDto>>> Handle(GetUsersWithRolesQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersWithRolesAsync();

            var userDtos = users.Select(u => new UserWithRolesDto(
                u.Id,
                u.Employee != null ? u.Employee.FullName : "N/A", 
                u.Email,
                u.UserRoles.Select(ur => ur.Role.RoleName).ToList()
            )).ToList();

            return Result<List<UserWithRolesDto>>.Success(userDtos);
        }
    }
}