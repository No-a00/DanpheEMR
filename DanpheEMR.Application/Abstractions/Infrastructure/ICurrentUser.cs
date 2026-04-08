
namespace DanpheEMR.Application.Abstractions.Infrastructure
{
    public interface ICurrentUser
    {
        Guid? UserId { get; }
        string? UserName { get; }
        string? Email { get; }
        bool IsAuthenticated { get; }
        IEnumerable<string> Roles { get; }
        bool IsInRole(string role);
    }
}