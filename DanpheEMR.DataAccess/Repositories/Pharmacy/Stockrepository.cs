using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Pharmacy;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Pharmacy
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Stock> _dbSet;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Stock>();
        }

        public async Task<Stock?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stock> AddAsync(Stock stock)
        {
            await _dbSet.AddAsync(stock);
            return stock;
        }

        public Task UpdateAsync(Stock stock)
        {
            _dbSet.Update(stock);
            return Task.CompletedTask;
        }
        public async Task<IEnumerable<Stock>> GetAvailableStocksAsync(Guid itemId, Guid storeId)
        {
            return await _dbSet.AsNoTracking()
                .Where(s => s.ItemId == itemId
                         && s.StoreId == storeId
                         && s.AvailableQuantity > 0 
                         && s.ExpiryDate >= DateTime.Now.Date) 
                                                               
                                                              
                .OrderBy(s => s.ExpiryDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<Stock>> GetStocksByItemIdAsync(Guid itemId)
        {
            return await _dbSet.AsNoTracking()
                .Where(s => s.ItemId == itemId && s.AvailableQuantity > 0)
                .OrderBy(s => s.ExpiryDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Stock>> GetStocksByStoreIdAsync(Guid storeId)
        {
            return await _dbSet.AsNoTracking()
                .Where(s => s.StoreId == storeId && s.AvailableQuantity > 0)
                
                .OrderBy(s => s.ItemId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Stock>> GetExpiringStocksAsync(Guid storeId, int daysToThreshold)
        {
            var thresholdDate = DateTime.Now.Date.AddDays(daysToThreshold);

            return await _dbSet.AsNoTracking()
                .Where(s => s.StoreId == storeId
                         && s.AvailableQuantity > 0
                         && s.ExpiryDate <= thresholdDate)
                .OrderBy(s => s.ExpiryDate) 
                .ToListAsync();
        }
    }
}