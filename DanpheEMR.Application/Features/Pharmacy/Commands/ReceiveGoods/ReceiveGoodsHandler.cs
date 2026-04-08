using Application.Common;
using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Base;
using DanpheEMR.Core.Interface.Pharmacy; // Chứa IStockRepository
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.Pharmacy.Commands.ReceiveGoods
{
    public class ReceiveGoodsHandler : IRequestHandler<ReceiveGoodsCommand, Result<Guid>>
    {
        private readonly IGenericRepository<GoodsReceipt> _receiptRepository;
        private readonly IStockRepository _stockRepository; // Inject thêm Repo tồn kho
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
               
                Guid mainStoreId = Guid.Parse("00000000-0000-0000-0000-000000000001");

                var receipt = new GoodsReceipt
                {
                    Id = Guid.NewGuid(),
                    SupplierId = request.SupplierId,
                    InvoiceNo = request.InvoiceNo,
                    ReceiptDate = request.ReceiptDate,
                    Remarks = request.Remarks,

                    Items = request.Items.Select(i => new GoodsReceiptItem
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

                // 1. Lưu chứng từ nhập kho
                await _receiptRepository.AddAsync(receipt);

                // 2. LOGIC CỘNG TỒN KHO
                foreach (var item in receipt.Items)
                {
                    int totalQty = item.ReceivedQuantity + item.FreeQuantity;

                    await _stockRepository.AddStockAsync(
                        mainStoreId,
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