using Application.Common;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interface.Base;
using DanpheEMR.Core.Interface.Pharmacy;
using MediatR;

namespace DanpheEMR.Application.Features.Pharmacy.Commands.DispenseDrugs
{
    public class DispenseDrugsHandler : IRequestHandler<DispenseDrugsCommand, Result<Guid>>
    {
        private readonly IGenericRepository<Dispense> _dispenseRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DispenseDrugsHandler(
            IGenericRepository<Dispense> dispenseRepository,
            IStockRepository stockRepository,
            IUnitOfWork unitOfWork)
        {
            _dispenseRepository = dispenseRepository; _stockRepository = stockRepository; _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DispenseDrugsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. KIỂM TRA TỒN KHO
                foreach (var item in request.Items)
                {
                    bool isAvailable = await _stockRepository.CheckStockAvailabilityAsync(request.StoreId, item.ItemId, item.BatchNo, item.Quantity);
                    if (!isAvailable)
                    {
                        return Result<Guid>.Failure(new Error("Dispense.NoStock", $"Thuốc có ID {item.ItemId} (Lô: {item.BatchNo}) đã hết hoặc không đủ để bán."));
                    }
                }

                // 2. TẠO HÓA ĐƠN XUẤT THUỐC
                var dispense = new Dispense
                {
                    Id = Guid.NewGuid(),
                    PatientId = request.PatientId,
                    PrescriptionId = request.PrescriptionId,
                    StoreId = request.StoreId,
                    DispenseDate = DateTime.Now,
                    // Tính tổng tiền hóa đơn
                    TotalAmount = request.Items.Sum(i => i.Quantity * i.SalePrice),

                    Items = request.Items.Select(i => new DispenseItem
                    {
                        Id = Guid.NewGuid(),
                        ItemId = i.ItemId,
                        BatchNo = i.BatchNo,
                        Quantity = i.Quantity,
                        SalePrice = i.SalePrice,
                        SubTotal = i.Quantity * i.SalePrice
                    }).ToList()
                };

                await _dispenseRepository.AddAsync(dispense);

                // 3. LOGIC TRỪ TỒN KHO
                foreach (var item in dispense.Items)
                {
                    await _stockRepository.DeductStockAsync(request.StoreId, item.ItemId, item.BatchNo, item.Quantity);
                }

                var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
                return saveResult > 0 ? Result<Guid>.Success(dispense.Id) : Result<Guid>.Failure(new Error("Dispense.DBError", "Lỗi lưu hóa đơn xuất thuốc."));
            }
            catch (Exception ex) { return Result<Guid>.Failure(new Error("Dispense.Ex", ex.Message)); }
        }
    }
}