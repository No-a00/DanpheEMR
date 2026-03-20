using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Pharmacy;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Pharmacy
{
    public class StockTransactionRepository : IStockTransactionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<StockTransaction> _dbSet;

        public StockTransactionRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<StockTransaction>();
        }
        public async Task<StockTransaction> AddAsync(StockTransaction transaction)
        {
            await _dbSet.AddAsync(transaction);
            return transaction;
        }
        public async Task<StockTransaction?> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<IEnumerable<StockTransaction>> GetTransactionsByItemAsync(int itemId, DateTime fromDate, DateTime toDate)
        {
            var start = fromDate.Date;
            var end = toDate.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()
                .Where(t => t.ItemId == itemId
                         && t.TransactionDate >= start
                         && t.TransactionDate <= end)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<StockTransaction>> GetTransactionsByStoreAsync(int storeId, DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = date.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()
                .Where(t => t.StoreId == storeId
                         && t.TransactionDate >= startOfDay
                         && t.TransactionDate <= endOfDay)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<StockTransaction>> GetTransactionsByReferenceNoAsync(string referenceNo)
        {
            return await _dbSet.AsNoTracking()
                .Where(t => t.ReferenceNo == referenceNo)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
    }
}