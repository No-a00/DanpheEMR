using MediatR;
namespace DanpheEMR.Application.Features.Auth.Commands.CreateUserAccount
{
    public record CreateUserAccountCommand(
        string UserName,
        string Code ,
        string Email,
        string PhoneNumber,
        string FullName,
        string EmailVerified,
        string Gender,
        DateTime DateOfBirth,
        string AvatarUrl,
        string Password
        
    ) : IRequest<Result<Guid>>;
}