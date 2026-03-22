
namespace DanpheEMR.Application.Abstractions.Infrastructure
{
    public interface ICurrentUser
    {
        Guid UserId { get; }
        string UserName { get; }
        string Email { get; }
        string Role { get; }
        bool IsAuthenticated { get; }
    }
}
