
using DanpheEMR.Core.Domain.Billing;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Billing;
using MediatR;
using DanpheEMR.Core.Enums; 


namespace DanpheEMR.Application.Features.Billing.Commands.ProcessPayment
{
    public class ProcessPaymentHandler : IRequestHandler<ProcessPaymentCommand, Result<ProcessPaymentResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBillingTransactionRepository _billingTransactionRepository;

        public ProcessPaymentHandler(IUnitOfWork uow, IBillingTransactionRepository billingTransactionRepository)
        {
            _uow = uow;
            _billingTransactionRepository = billingTransactionRepository;
        }
        public async Task<Result<ProcessPaymentResponse>> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            // 2. Lấy hóa đơn gốc từ Database
            var invoice = await _billingTransactionRepository.GetByIdAsync(request.InvoiceId);
            if (invoice == null)
            {
                return Result<ProcessPaymentResponse>.Failure(ProcessPaymentErrors.InvoiceNotFound);
            }
            if (invoice.PaymentStatus == PaymentStatus.Paid)
            {
                return Result<ProcessPaymentResponse>.Failure(ProcessPaymentErrors.AlreadyPaid);
            }

            // 3. Tạo bản ghi giao dịch thanh toán
            var payment = new BillingTransaction
            {
                Id = Guid.NewGuid(),
                VisitId = invoice.VisitId,
                PatientId = invoice.PatientId,

                SubTotal = request.AmountPaid,
                TotalAmount = request.AmountPaid,

                PaymentMode = Enum.Parse<PaymentMode>(request.PaymentMethod),
                TransactionType = TransactionType.Sales,

                TransactionDate = DateTime.UtcNow,
                PaymentStatus = PaymentStatus.Paid,
                IsActive = true,
                InvoiceNumber = $"INV-{DateTime.Now:yyyyMMddHHmm}"
            };
            invoice.PaymentStatus = PaymentStatus.Paid;
            
            await _billingTransactionRepository.AddAsync(payment);
            await _uow.SaveChangesAsync(cancellationToken);
            return Result.Success(new ProcessPaymentResponse
            {
                PaymentId = payment.Id,
                NewInvoiceStatus = invoice.PaymentStatus.ToString()
            });
        }
    }
}