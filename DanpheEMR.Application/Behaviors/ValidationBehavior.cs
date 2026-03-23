using FluentValidation;
using MediatR;


namespace DanpheEMR.Application.Behaviors
{
    // Đây là Pipeline Behavior của MediatR, nó sẽ đứng chặn giữa Controller và CommandHandler
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> // Đảm bảo chỉ áp dụng cho các Request của MediatR
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        // Tiêm tất cả các Validator mà bạn đã viết (ví dụ: BookAppointmentCommandValidator) vào đây
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // 1. Nếu Command này không có file Validator nào, cho đi tiếp luôn
            if (!_validators.Any())
            {
                return await next();
            }

            // 2. Gom dữ liệu của Command lại
            var context = new ValidationContext<TRequest>(request);

            // 3. Chạy qua TẤT CẢ các file Validator liên quan cùng một lúc
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // 4. Gom tất cả các lỗi tìm được (nếu có)
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            // 5. Nếu có dù chỉ 1 lỗi, lập tức đá văng ra ngoài, KHÔNG cho đi vào CommandHandler!
            if (failures.Any())
            {
                // Quăng lỗi của FluentValidation. 
                // Sau này ở Web API, ta sẽ dùng 1 Middleware để bắt lỗi này và biến nó thành mã 400 Bad Request
                throw new ValidationException(failures);
            }

            // 6. Nếu dữ liệu sạch sẽ 100%, cho phép đi tiếp vào CommandHandler
            return await next();
        }
    }
}