
namespace DanpheEMR.Application.Abstractions.Services.Admin
{
    public interface IAuthService
    {
        Task <string> LoginAsync(string username, string password,CancellationToken cancellationToken );
        Task  LogoutAsync( Guid userId,CancellationToken cancellationToken );
        Task<string> RefreshTokenAsync(string token, CancellationToken cancellationToken);
        Task ChangePasswordAsync(Guid userId,string oldPassword,string newPassword,CancellationToken cancellationToken);

    }
}
