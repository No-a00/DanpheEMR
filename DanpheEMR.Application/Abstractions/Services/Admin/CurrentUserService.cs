
using DanpheEMR.Core.Interfaces.Base;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DanpheEMR.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid UserId
        {
            get
            {
                // Lấy ID từ Token của người dùng đang đăng nhập
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!string.IsNullOrEmpty(userIdClaim) && Guid.TryParse(userIdClaim, out Guid userId))
                {
                    return userId;
                }

                return Guid.Empty;
            }
        }
    }
}