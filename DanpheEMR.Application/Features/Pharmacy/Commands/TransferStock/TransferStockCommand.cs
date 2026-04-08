using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.Pharmacy.Commands.TransferStock
{
    public record TransferStockCommand(
        Guid FromStoreId,
        Guid ToStoreId,
        string Remarks,
        List<TransferItemDto> Items
    ) : IRequest<Result<Guid>>;

    public record TransferItemDto(Guid ItemId, string BatchNo, int Quantity);
}