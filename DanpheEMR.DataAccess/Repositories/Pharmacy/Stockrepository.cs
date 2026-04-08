using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Pharmacy;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Pharmacy
{
    public class StockRepository : GenericRepository<Stock>, IStockRepository
    {

        public StockRepository(ApplicationDbContext context) : base(context) { }
        public async Task<IEnumerable<Stock>> GetAvailableStocksAsync(Guid itemId, Guid storeId)
        {
            return await _dbSet.AsNoTracking()
                .Where(s => s.ItemId == itemId
                         && s.StoreId == storeId
                         && !s.IsDeleted
                         && s.AvailableQuantity > 0 
                         && s.ExpiryDate >= DateTime.Now.Date) 
                                                               
                                                              
                .OrderBy(s => s.ExpiryDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<Stock>> GetStocksByItemIdAsync(Guid itemId)
        {
            return await _dbSet.AsNoTracking()
                .Where(s => s.ItemId == itemId && !s.IsDeleted && s.AvailableQuantity > 0)
                .OrderBy(s => s.ExpiryDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Stock>> GetStocksByStoreIdAsync(Guid storeId)
        {
            return await _dbSet.AsNoTracking()
                .Where(s => s.StoreId == storeId && !s.IsDeleted && s.AvailableQuantity > 0)
                
                .OrderBy(s => s.ItemId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Stock>> GetExpiringStocksAsync(Guid storeId, int daysToThreshold)
        {
            var thresholdDate = DateTime.Now.Date.AddDays(daysToThreshold);

            return await _dbSet.AsNoTracking()
                .Where(s => s.StoreId == storeId
                         && s.AvailableQuantity > 0
                         && !s.IsDeleted
                         && s.ExpiryDate <= thresholdDate)
                .OrderBy(s => s.ExpiryDate) 
                .ToListAsync();
        }
    }
}