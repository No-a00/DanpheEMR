using DanpheEMR.Core.Domain.Admin;
using System.Diagnostics;

namespace DanpheEMR.WEB.Middleware
{
    public class RequestAuditMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestAuditMiddleware> _logger;
        public RequestAuditMiddleware(RequestDelegate next, ILogger<RequestAuditMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            var userId = context.User.Identity?.IsAuthenticated == true 
                ? context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value 
                : "Anonymous";

            await _next(context);
            sw.Stop();

            // Bạn có thể nâng cấp đoạn này để lưu vào Database bảng AuditLogs nếu cần
            _logger.LogInformation(
                "Audit Log: HTTP {Method} {Path} responded {StatusCode} in {Elapsed}ms. User: {UserId}, IP: {Ip}",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                sw.ElapsedMilliseconds,
                userId,
                ipAddress);

        }
    }
}
