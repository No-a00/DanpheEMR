namespace DanpheEMR.WEB.Middleware
{
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                var headers = context.Response.Headers;

                // Ngăn chặn bị nhúng vào iframe (Chống Clickjacking)
                if (!headers.ContainsKey("X-Frame-Options"))
                    headers.Add("X-Frame-Options", "DENY");

                // Ép trình duyệt tôn trọng Content-Type
                if (!headers.ContainsKey("X-Content-Type-Options"))
                    headers.Add("X-Content-Type-Options", "nosniff");

                // Kích hoạt bộ lọc XSS của trình duyệt
                if (!headers.ContainsKey("X-XSS-Protection"))
                    headers.Add("X-XSS-Protection", "1; mode=block");

                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}

