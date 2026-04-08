using System;

namespace DanpheEMR.Application.Features.Pharmacy.Queries.GetCurrentStock
{
    public record GetCurrentStockResponse(
        Guid Id,
        Guid StoreId,
        string StoreName,
        Guid ItemId,
        string ItemName, 
        string BatchNo,   
        DateTime ExpiryDate, 
        int AvailableQuantity 
    );
}