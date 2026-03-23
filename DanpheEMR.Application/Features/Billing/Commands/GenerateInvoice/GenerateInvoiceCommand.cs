using MediatR;

namespace DanpheEMR.Application.Features.Billing.Commands.GenerateInvoice
{
    // Command tổng thể cho hóa đơn
    public record GenerateInvoiceCommand(
        Guid VisitId,
        Guid PatientId,
        string? Remarks, // Ghi chú thêm của thu ngân
        List<InvoiceItemDto> Items
    ) : IRequest<Result<GenerateInvoiceResponse>>;

    // DTO chi tiết từng dịch vụ (Snapshot)
    public record InvoiceItemDto(
        Guid ServiceItemId,
        string ServiceName,
        decimal Quantity, // Chuyển sang decimal để chính xác tuyệt đối
        decimal UnitPrice,
        decimal DiscountAmount, // Giảm giá riêng từng dòng
        decimal TaxAmount,      // Thuế riêng từng dòng
        Guid? ProviderId        // Bác sĩ chỉ định/thực hiện (để tính hoa hồng)
    );
}