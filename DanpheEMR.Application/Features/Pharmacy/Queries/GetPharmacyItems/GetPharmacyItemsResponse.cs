using System;

namespace DanpheEMR.Application.Features.Pharmacy.Queries.GetPharmacyItems
{
    public record GetPharmacyItemsResponse(
        Guid Id,
        string ItemCode,
        string ItemName,
        string GenericName,
        string UOM, 
        Guid CategoryId
    );
}