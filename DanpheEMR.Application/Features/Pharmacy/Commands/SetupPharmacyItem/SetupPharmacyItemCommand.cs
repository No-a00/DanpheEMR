using Application.Common;
using MediatR;
using System;

namespace DanpheEMR.Application.Features.Pharmacy.Commands.SetupPharmacyItem
{
    public record SetupPharmacyItemCommand(
        string ItemCode,
        string ItemName,
        string GenericName,
        string UOM, 
        Guid CategoryId 
    ) : IRequest<Result<Guid>>;
}