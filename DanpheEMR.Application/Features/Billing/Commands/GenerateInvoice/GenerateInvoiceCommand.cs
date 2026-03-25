using MediatR;

namespace DanpheEMR.Application.Features.Billing.Commands.GenerateInvoice
{
    public record GenerateInvoiceCommand(
        Guid VisitId,
        Guid PatientId,
        string? Remarks, // Ghi chú thêm của thu ngân
        List<InvoiceItemDto> Items
    ) : IRequest<Result<GenerateInvoiceResponse>>;

    public record InvoiceItemDto(
        Guid ServiceItemId,
        string ServiceName,
        decimal Quantity, 
        decimal UnitPrice,
        decimal DiscountAmount, 
        decimal TaxAmount,      
        Guid? ProviderId        // Bác sĩ chỉ định/thực hiện (để tính hoa hồng)
    );
}