using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Interfaces.Pharmacy;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Pharmacy
{
    public class GoodsReceiptRepository : GenericRepository<GoodsReceipt>, IGoodsReceiptRepository
    {

        public GoodsReceiptRepository(ApplicationDbContext context) : base(context) { }
        public async Task<IEnumerable<GoodsReceipt>> GetPendingReceiptsAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(g => !g.IsDeleted && g.Status == GoodsReceiptStatus.Pending)
                .OrderByDescending(x => x.ReceiptDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<GoodsReceipt>> GetReceiptsByDateAsync(DateTime fromDate, DateTime toDate)
        {
            var start = fromDate.Date;
            var end = toDate.Date.AddDays(1).AddTicks(-1); 

            return await _dbSet.AsNoTracking()
                .Where(g => !g.IsDeleted && g.ReceiptDate >= start && g.ReceiptDate <= end)
                .OrderByDescending(g => g.ReceiptDate)
                .ToListAsync();
        }
        public async Task<GoodsReceipt?> GetReceiptWithItemsAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Include(g => g.GoodsReceiptItems)
                .FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted);
        }
    }
}