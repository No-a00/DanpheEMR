using Application.Common;
using DanpheEMR.Application.Common.Models;
using DanpheEMR.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace DanpheEMR.WEB.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Có lỗi hệ thống xảy ra: {Message}", ex.Message);

                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = " application/json";
            var statusCode = HttpStatusCode.InternalServerError;
            var errors = new List<Error>();
            string message = "Đã có lỗi hệ thống xảy ra. Vui lòng liên hệ quản trị viên.";

            switch (exception)
            {
                case NotFoundException notFoundEx:
                    statusCode = HttpStatusCode.NotFound;
                    message = "Không tìm thấy tài nguyên.";
                    errors.Add(new Error("Resoure.NotFound", notFoundEx.Message));
                    break;
                case  BusinessException businessEx:
                        statusCode = HttpStatusCode.BadRequest;
                        message = "Lỗi nghiệp vụ.";
                        errors.Add(new Error("BusinessError", businessEx.Message));
                    break;
                case UnauthorizedAccessException unauthorizedEx:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "Bạn không có quyền thực hiện thao tác này.";
                    errors.Add(new Error("Auth.Unauthorized", message));
                    break;
                default:
                    errors.Add(new Error("System.InternalError", "Lỗi nội bộ máy chủ."));
                    break;

            }
            context.Response.StatusCode = (int)statusCode;
            var response = ApiResponse<object>.Failure(errors, message);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var json = JsonSerializer.Serialize(response, options);
            return context.Response.WriteAsync(json);

        }
    }
}
