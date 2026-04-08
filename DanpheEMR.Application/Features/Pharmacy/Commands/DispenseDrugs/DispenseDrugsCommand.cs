using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.Pharmacy.Commands.DispenseDrugs
{
    public record DispenseDrugsCommand(
        Guid? PatientId,
        Guid? PrescriptionId,
        Guid StoreId, 
        List<DispenseItemDto> Items
    ) : IRequest<Result<Guid>>;

    public record DispenseItemDto(Guid ItemId, string BatchNo, int Quantity, decimal SalePrice);
}