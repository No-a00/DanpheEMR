using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Pharmacy;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Pharmacy
{
    public class StockTransactionRepository : GenericRepository<StockTransaction>, IStockTransactionRepository
    {
        public StockTransactionRepository(ApplicationDbContext context) : base(context) { }
        public async Task<IEnumerable<StockTransaction>> GetTransactionsByItemAsync(Guid itemId, DateTime fromDate, DateTime toDate)
        {
            var start = fromDate.Date;
            var end = toDate.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()
                .Where(t => t.ItemId == itemId
                         && t.TransactionDate >= start
                         && !t.IsDeleted
                         && t.TransactionDate <= end)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<StockTransaction>> GetTransactionsByStoreAsync(Guid storeId, DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = date.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()
                .Where(t => t.StoreId == storeId
                         && !t.IsDeleted
                         && t.TransactionDate >= startOfDay
                         && t.TransactionDate <= endOfDay)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<StockTransaction>> GetTransactionsByReferenceNoAsync(string referenceNo)
        {
            return await _dbSet.AsNoTracking()
                .Where(t => t.ReferenceNo == referenceNo && !t.IsDeleted)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
    }
}