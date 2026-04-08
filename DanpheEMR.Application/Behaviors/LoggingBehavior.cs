using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using DanpheEMR.Application.Abstractions.Infrastructure; // Mở nếu dùng ICurrentUser

namespace DanpheEMR.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly ICurrentUser _currentUser; // Inject vào để biết ai đang gọi API

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUser.UserId?.ToString() ?? "Anonymous";

            _logger.LogInformation("[START] Xử lý Request: {RequestName}", requestName);
            // Dùng để log chi tiết payload (chú ý cẩn thận với dữ liệu nhạy cảm)
            _logger.LogInformation("Payload: {@Request}", request);

            var timer = Stopwatch.StartNew();

            // Cho phép Request đi tiếp vào Handler
            var response = await next();

            timer.Stop();

            _logger.LogInformation("[END] Hoàn thành Request: {RequestName} trong {ElapsedMilliseconds}ms", requestName, timer.ElapsedMilliseconds);

            return response;
        }
    }
}