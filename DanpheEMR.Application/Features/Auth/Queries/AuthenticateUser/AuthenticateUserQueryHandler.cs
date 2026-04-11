using DanpheEMR.Application.Abstractions.Services.Admin; 
using MediatR;


namespace DanpheEMR.Application.Features.Auth.Queries.AuthenticateUser
{
    public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, Result<AuthenticateUserResponse>>
    {
        private readonly IAuthService _authService; 

        public AuthenticateUserQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Result<AuthenticateUserResponse>> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
        {
            
            var response = await _authService.LoginAsync(request.Username, request.Password, cancellationToken);

            if (response == null)
            {
              
                return Result<AuthenticateUserResponse>.Failure(AuthenticateUserErrors.InvalidCredentials);
            }

            return Result<AuthenticateUserResponse>.Success(response);
        }
    }
}