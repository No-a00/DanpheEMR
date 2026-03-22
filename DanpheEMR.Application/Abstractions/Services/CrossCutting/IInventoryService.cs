namespace DanpheEMR.Application.Abstractions.Services.CrossCutting
{
    public interface IInventoryService
    {
        Task<bool> CheckStockAsync(Guid itemId, int quantity);
        Task DeductStockAsync(Guid itemId, int quantity);
        Task AddStockAsync(Guid itemId, int quantity);
    }
}
