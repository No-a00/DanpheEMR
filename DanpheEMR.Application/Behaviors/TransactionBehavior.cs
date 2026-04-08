using DanpheEMR.Application.Abstractions.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanpheEMR.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

        public TransactionBehavior(IUnitOfWork unitOfWork, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            if (!requestName.EndsWith("Command"))
            {
                return await next();
            }

            _logger.LogInformation("[TRANSACTION] Bắt đầu Transaction cho {RequestName}", requestName);

            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                // Chạy logic nghiệp vụ trong Handler
                var response = await next();

                // Nếu Handler chạy êm xuôi thì Lưu tất cả xuống DB
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                _logger.LogInformation("[TRANSACTION] Đã Commit Transaction cho {RequestName}", requestName);

                return response;
            }
            catch (Exception)
            {
                _logger.LogWarning("[TRANSACTION] Xảy ra lỗi, tiến hành Rollback Transaction cho {RequestName}", requestName);
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}