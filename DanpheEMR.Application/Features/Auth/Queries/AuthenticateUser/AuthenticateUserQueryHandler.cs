
using DanpheEMR.Core.Interface.Admin; 
using DanpheEMR.Core.Interface.Auth;
using MediatR;


namespace DanpheEMR.Application.Features.Auth.Queries.AuthenticateUser
{
    public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, Result<AuthenticateUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public AuthenticateUserQueryHandler(
            IUserRepository userRepository,
            IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<AuthenticateUserResponse>> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);

            if (user == null)
            {
                return Result<AuthenticateUserResponse>.Failure(AuthenticateUserErrors.InvalidCredentials);
            }

            if (!user.IsActive)
            {
                return Result<AuthenticateUserResponse>.Failure(AuthenticateUserErrors.AccountLocked);
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return Result<AuthenticateUserResponse>.Failure(AuthenticateUserErrors.InvalidCredentials);
            }

           
            string token = _jwtProvider.GenerateToken(user);

          
            var response = new AuthenticateUserResponse
            {
                UserId = user.Id,
                Username = user.UserName,
                EmployeeId = user.EmployeeId,
                Token = token
            };

            return Result<AuthenticateUserResponse>.Success(response);
        }
    }
}