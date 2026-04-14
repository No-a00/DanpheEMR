using DanpheEMR.Core.Interfaces.Base;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DanpheEMR.Api.Services
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
                var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Guid.TryParse(userIdString, out var guid) ? guid : Guid.Empty;
            }
        }

        public string? UserName => _httpContextAccessor.HttpContext?.User?.Identity?.Name;

       
        public string? IpAddress
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                if (context == null) return null;

                if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
                {
                    var forwardedHeader = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                    if (!string.IsNullOrEmpty(forwardedHeader))
                    {
                        return forwardedHeader.Split(',')[0].Trim();
                    }
                }
                return context.Connection.RemoteIpAddress?.ToString();
            }
        }
        public string? CorrelationId
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                if (context == null) return null;

                if (context.Request.Headers.TryGetValue("X-Correlation-ID", out var correlationId))
                {
                    return correlationId.FirstOrDefault();
                }

                
                return Guid.NewGuid().ToString();
            }
        }
    }
}