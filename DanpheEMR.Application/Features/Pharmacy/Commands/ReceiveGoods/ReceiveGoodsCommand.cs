using MediatR;


namespace DanpheEMR.Application.Features.Pharmacy.Commands.ReceiveGoods
{
    public record ReceiveGoodsCommand(
        Guid SupplierId,
        Guid StoreId, 
        string InvoiceNo,
        DateTime ReceiptDate,
        string Remarks,
        List<ReceiptItemDto> Items
    ) : IRequest<Result<Guid>>;

    public record ReceiptItemDto(
        Guid ItemId,
        string BatchNo,
        DateTime ExpiryDate,
        int ReceivedQuantity,
        int FreeQuantity,
        decimal PurchaseRate,
        decimal Margin
    );
}