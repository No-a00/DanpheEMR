using Application.Common;
using DanpheEMR.Core.Interface.BloodBank;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DanpheEMR.Application.Features.BloodBank.Queries.GetBloodInventory
{
    public class GetBloodInventoryQueryHandler : IRequestHandler<GetBloodInventoryQuery, Result<GetBloodInventoryResponse>>
    {
        private readonly IBloodInventoryRepository _inventoryRepository;

        public GetBloodInventoryQueryHandler(IBloodInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<Result<GetBloodInventoryResponse>> Handle(GetBloodInventoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
              
                var availableBags = await _inventoryRepository.GetAllAvailableBagsAsync(request.BloodGroupId);

                var bagDtos = availableBags.ToDtoList();

                var response = new GetBloodInventoryResponse
                {
                    TotalAvailableBags = bagDtos.Count,
                    Bags = bagDtos
                };

                return Result<GetBloodInventoryResponse>.Success(response);
            }
            catch (Exception)
            {
                return Result<GetBloodInventoryResponse>.Failure(new Error("GetInventory.Error", "Đã xảy ra lỗi khi tải dữ liệu kho máu."));
            }
        }
    }
}