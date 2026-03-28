using Application.Common;
using MediatR;

namespace DanpheEMR.Application.Features.Auth.Queries.AuthenticateUser
{
    public record AuthenticateUserQuery(
        string Username,
        string Password 
    ) : IRequest<Result<AuthenticateUserResponse>>;
}