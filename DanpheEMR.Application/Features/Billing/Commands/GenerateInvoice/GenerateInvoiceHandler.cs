using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Billing;
using DanpheEMR.Core.Interface.Billing;
using MediatR;
namespace DanpheEMR.Application.Features.Billing.Commands.GenerateInvoice
{
    public class GenerateInvoiceHandler : IRequestHandler<GenerateInvoiceCommand, Result<GenerateInvoiceResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBillingTransactionRepository _billingTransactionRepository;

        public GenerateInvoiceHandler(IUnitOfWork uow,IBillingTransactionRepository billingTransactionRepository)
        {
            _uow = uow;
            _billingTransactionRepository = billingTransactionRepository;
        }

        public async Task<Result<GenerateInvoiceResponse>> Handle(GenerateInvoiceCommand request, CancellationToken ct)
        {
            // 1. Logic tính toán tài chính
            // SubTotal = Σ(Quantity * UnitPrice)
            decimal subTotal = request.Items.Sum(i => i.Quantity * i.UnitPrice);
            decimal discount = 0; // Bạn có thể thêm logic tính discount ở đây nếu cần
            decimal tax = subTotal * 0.1m; // Giả sử VAT 10%
            decimal total = subTotal - discount + tax;

            // 2. Khởi tạo Entity theo đúng class BillingTransaction bạn đã định nghĩa
            var transaction = new BillingTransaction
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = $"INV-{DateTime.Now:yyyyMMddHHmm}",
                TransactionDate = DateTime.UtcNow,
                PatientId = request.PatientId,
                VisitId = request.VisitId,

                // Các trường tiền tệ decimal(18,2)
                SubTotal = subTotal,
                DiscountAmount = discount,
                TaxAmount = tax,
                TotalAmount = total,

                PaymentStatus = Core.Enums.PaymentStatus.Pending,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            // 3. Thêm các dòng chi tiết vào TransactionItems (HashSet)
            foreach (var item in request.Items)
            {
                var detailItem = new BillingTransactionItem
                {
                    Id = Guid.NewGuid(),
                    // BillingTransactionId sẽ tự động được EF gán khi bạn add vào collection

                    ServiceItemId = item.ServiceItemId, 
                    ItemName = item.ServiceName, // Snapshot tên dịch vụ
                    Quantity = item.Quantity,
                    Price = item.UnitPrice, // Snapshot đơn giá

                    // Tính toán con số trên từng dòng
                    SubTotal = (decimal)item.Quantity * item.UnitPrice,
                    DiscountAmount = item.DiscountAmount,
                    TaxAmount = item.TaxAmount,
                    TotalAmount = ((decimal)item.Quantity * item.UnitPrice) - item.DiscountAmount + item.TaxAmount,

                    // Gắn bác sĩ chỉ định/thực hiện để tính hoa hồng sau này
                    ProviderId = item.ProviderId
                };

                transaction.TransactionItems.Add(detailItem);
            }

            await _billingTransactionRepository.AddAsync(transaction);

            //  Lưu tất cả vào Database trong một Transaction duy nhất
            await _uow.SaveChangesAsync(ct);

            return Result.Success(new GenerateInvoiceResponse
            {
                InvoiceId = transaction.Id,
                InvoiceNumber = transaction.InvoiceNumber,
                TotalAmount = total
            });
        }
    }
}