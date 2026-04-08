using Application.Common;
using DanpheEMR.Application.Abstractions.Persistence;
using DanpheEMR.Core.Domain.Pharmacy; // Giả định Entity StockTransfer
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Base;
using DanpheEMR.Core.Interface.Pharmacy;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.Pharmacy.Commands.TransferStock
{
    public class TransferStockHandler : IRequestHandler<TransferStockCommand, Result<Guid>>
    {
        private readonly IGenericRepository<StockTransfer> _transferRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TransferStockHandler(
            IGenericRepository<StockTransfer> transferRepository,
            IStockRepository stockRepository,
            IUnitOfWork unitOfWork)
        {
            _transferRepository = transferRepository; _stockRepository = stockRepository; _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(TransferStockCommand request, CancellationToken cancellationToken)
        {
            try
            {
     
                foreach (var item in request.Items)
                {
                    bool isAvailable = await _stockRepository.CheckStockAvailabilityAsync(request.FromStoreId, item.ItemId, item.BatchNo, item.Quantity);
                    if (!isAvailable)
                    {
                        return Result<Guid>.Failure(new Error("Transfer.NoStock", $"Thuốc/Vật tư có ID {item.ItemId} (Lô: {item.BatchNo}) không đủ tồn kho để chuyển."));
                    }
                }

           
                var transfer = new StockTransfer
                {
                    Id = Guid.NewGuid(),
                    FromStoreId = request.FromStoreId,
                    ToStoreId = request.ToStoreId,
                    TransferDate = DateTime.Now,
                    Remarks = request.Remarks,
                    Items = request.Items.Select(i => new StockTransferItem
                    {
                        Id = Guid.NewGuid(),
                        ItemId = i.ItemId,
                        BatchNo = i.BatchNo,
                        Quantity = i.Quantity
                    }).ToList()
                };

                await _transferRepository.AddAsync(transfer);

                foreach (var item in transfer.Items)
                {
                    // Lấy ngày hết hạn từ lô hiện tại để chuyển sang kho mới
                    var expiryDate = await _stockRepository.GetExpiryDateAsync(request.FromStoreId, item.ItemId, item.BatchNo);

                    // Trừ kho xuất
                    await _stockRepository.DeductStockAsync(request.FromStoreId, item.ItemId, item.BatchNo, item.Quantity);

                    // Cộng kho nhập
                    await _stockRepository.AddStockAsync(request.ToStoreId, item.ItemId, item.BatchNo, expiryDate, item.Quantity);
                }

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
                return saveResult > 0 ? Result<Guid>.Success(transfer.Id) : Result<Guid>.Failure(new Error("Transfer.DBError", "Lỗi lưu phiếu chuyển kho."));
            }
            catch (Exception ex) { return Result<Guid>.Failure(new Error("Transfer.Ex", ex.Message)); }
        }
    }
}