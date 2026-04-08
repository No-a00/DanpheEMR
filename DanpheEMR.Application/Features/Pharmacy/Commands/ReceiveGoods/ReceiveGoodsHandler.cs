using Application.Common;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Base;
using DanpheEMR.Core.Interface.Pharmacy;
using MediatR;


namespace DanpheEMR.Application.Features.Pharmacy.Commands.ReceiveGoods
{
    public class ReceiveGoodsHandler : IRequestHandler<ReceiveGoodsCommand, Result<Guid>>
    {
        private readonly IGenericRepository<GoodsReceipt> _receiptRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReceiveGoodsHandler(
            IGenericRepository<GoodsReceipt> receiptRepository,
            IStockRepository stockRepository,
            IUnitOfWork unitOfWork)
        {
            _receiptRepository = receiptRepository;
            _stockRepository = stockRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(ReceiveGoodsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var receipt = new GoodsReceipt
                {
                    Id = Guid.NewGuid(),
                    SupplierId = request.SupplierId,
                    StoreId = request.StoreId,

            
                    GoodsReceiptNo = $"GR-{DateTime.Now:yyyyMMdd}-{new Random().Next(1000, 9999)}",

                    InvoiceNo = request.InvoiceNo,
                    ReceiptDate = request.ReceiptDate,
                    Remarks = request.Remarks,

                    IsDeleted = false,
                    GoodsReceiptItems = request.Items.Select(i => new GoodsReceiptItem
                    {
                        Id = Guid.NewGuid(),
                        ItemId = i.ItemId,
                        BatchNo = i.BatchNo,
                        ExpiryDate = i.ExpiryDate,
                        ReceivedQuantity = i.ReceivedQuantity,
                        FreeQuantity = i.FreeQuantity,
                        PurchaseRate = i.PurchaseRate,
                        Margin = i.Margin,
                        SellingPrice = i.PurchaseRate + (i.PurchaseRate * i.Margin / 100),
                        SubTotal = i.ReceivedQuantity * i.PurchaseRate
                    }).ToList()
                };

                receipt.TotalAmount = receipt.GoodsReceiptItems.Sum(x => x.SubTotal);

              
                await _receiptRepository.AddAsync(receipt);

            
                foreach (var item in receipt.GoodsReceiptItems)
                {
                    int totalQty = item.ReceivedQuantity + item.FreeQuantity;

                    await _stockRepository.AddStockAsync(
                        receipt.StoreId,
                        item.ItemId,
                        item.BatchNo,
                        item.ExpiryDate,
                        totalQty
                    );
                }

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
                return saveResult > 0 ? Result<Guid>.Success(receipt.Id) : Result<Guid>.Failure(new Error("Receive.DBError", "Lỗi lưu phiếu nhập."));
            }
            catch (Exception ex) { return Result<Guid>.Failure(new Error("Receive.Ex", ex.Message)); }
        }
    }
}