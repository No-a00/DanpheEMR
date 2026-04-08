using MediatR;
using Microsoft.Extensions.Logging;
using System;

namespace DanpheEMR.Application.Behaviors
{
    public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<ExceptionBehavior<TRequest, TResponse>> _logger;

        public ExceptionBehavior(ILogger<ExceptionBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;

                // Log lại toàn bộ Exception (rất quan trọng để Debug)
                _logger.LogError(ex, "[SYSTEM ERROR] Lỗi hệ thống khi xử lý Request {RequestName}", requestName);

                // Sau khi log, ném lỗi ra ngoài để Exception Middleware của API bắt được và trả về HTTP 500 cho Frontend
                throw;
            }
        }
    }
}