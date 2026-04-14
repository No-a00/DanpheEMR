
using DanpheEMR.Core.Interfaces.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanpheEMR.Application.Behaviors
{
    public class AuditBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<AuditBehavior<TRequest, TResponse>> _logger;
        private readonly ICurrentUserService _currentUserService;

        public AuditBehavior(
            ILogger<AuditBehavior<TRequest, TResponse>> logger,
            ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // 1. Lấy thông tin ngữ cảnh
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId != Guid.Empty ? _currentUserService.UserId : Guid.NewGuid();
            var userName = _currentUserService.UserName ?? "Unknown";
            var ipAddress = _currentUserService.IpAddress ?? "Unknown IP";
            var correlationId = _currentUserService.CorrelationId ?? "N/A";
            // 2. Ghi log trước khi thực thi
            _logger.LogInformation(
                "Audit Log: [CorrelationId: {CorrelationId}] - IP: {IpAddress} - User: {UserId} ({UserName}) is executing {RequestName}.",
                correlationId, ipAddress, userId, userName, requestName);

            
            var response = await next();
            _logger.LogInformation(
                "Audit Log: [CorrelationId: {CorrelationId}] - {RequestName} executed successfully.",
                correlationId, requestName);

            return response;
        }
    }
}