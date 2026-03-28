using DanpheEMR.Core.Domain.Admin;

namespace DanpheEMR.Core.Interface.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}