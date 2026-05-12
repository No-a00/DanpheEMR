using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Billing;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Base;
using DanpheEMR.Core.Interface.Billing;
using Application.Common;
using MediatR;
using DanpheEMR.Core.Domain.Admin;
namespace DanpheEMR.Application.Features.Billing.Commands.GenerateInvoice
{
    public class GenerateInvoiceHandler : IRequestHandler<GenerateInvoiceCommand, Result<GenerateInvoiceResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBillingTransactionRepository _billingTransactionRepository;
        private readonly IGenericRepository<Patient> _patientRepository;
        private readonly IGenericRepository<Visit> _visitRepository;
        private readonly IGenericRepository<Employee> _employeeRepo;

        public GenerateInvoiceHandler(IUnitOfWork uow,IBillingTransactionRepository billingTransactionRepository, IGenericRepository<Employee> employeeRepo,
            IGenericRepository<Patient> patientRepository, IGenericRepository<Visit> visitRepository
            )
        {
            _uow = uow;
            _billingTransactionRepository = billingTransactionRepository;
            _patientRepository = patientRepository;
            _visitRepository = visitRepository;
            _employeeRepo = employeeRepo;
        }

        public async Task<Result<GenerateInvoiceResponse>> Handle(GenerateInvoiceCommand request, CancellationToken ct)
        {
            try
            {

                var patient = await _patientRepository.GetFirstOrDefaultAsync(p => p.PatientCode == request.PatientCode);
                if (patient == null) return Result<GenerateInvoiceResponse>.Failure(new Error("PatientNotFound", $"Không tìm thấy bệnh nhân với mã {request.PatientCode}"));

                var visit = await _visitRepository.GetFirstOrDefaultAsync(v => v.VisitCode == request.VisitCode);
                if (visit == null) return Result<GenerateInvoiceResponse>.Failure(new Error("VisitNotFound", $"Không tìm thấy lượt khám với mã {request.VisitCode}"));
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
                    PatientId = patient.Id,
                    VisitId = visit.Id,

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
                    var provider = await _employeeRepo.GetFirstOrDefaultAsync(e => e.Code == item.ProviderCode);
                    var detailItem = new BillingTransactionItem
                    {
                        Id = Guid.NewGuid(),
                        // BillingTransactionId sẽ tự động được EF gán khi bạn add vào collection
                        ItemName = item.ServiceName, // Snapshot tên dịch vụ
                        Quantity = item.Quantity,
                        Price = item.UnitPrice, // Snapshot đơn giá

                        // Tính toán con số trên từng dòng
                        SubTotal = (decimal)item.Quantity * item.UnitPrice,
                        DiscountAmount = item.DiscountAmount,
                        TaxAmount = item.TaxAmount,
                        TotalAmount = ((decimal)item.Quantity * item.UnitPrice) - item.DiscountAmount + item.TaxAmount,

                        // Gắn bác sĩ chỉ định/thực hiện để tính hoa hồng sau này
                        ProviderId = provider?.Id
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
            catch (Exception ex)
            {

                return Result<GenerateInvoiceResponse>.Failure(new Error("GenerateInvoice.Exception", $"lỗi khi tạo hóa đơn thanh toán {ex.Message}"));
            }
        }
    }
}