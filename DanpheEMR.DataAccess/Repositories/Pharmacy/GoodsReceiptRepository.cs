using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Domain.Pharmacy;
// Nhớ using Interface vào nhé
using DanpheEMR.Core.Interfaces.Pharmacy;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Pharmacy
{
    // Đã thêm kế thừa Interface
    public class GoodsReceiptRepository : IGoodsReceiptRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<GoodsReceipt> _dbSet;

        public GoodsReceiptRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<GoodsReceipt>();
        }

        public async Task<GoodsReceipt?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<GoodsReceipt> AddAsync(GoodsReceipt goodsReceipt)
        {
            await _dbSet.AddAsync(goodsReceipt);
            return goodsReceipt;
        }

        public Task UpdateAsync(GoodsReceipt goodsReceipt)
        {
            _dbSet.Update(goodsReceipt);
            return Task.CompletedTask;
        }

        public async Task CancelReceiptAsync(Guid id, string cancelReason, Guid cancelledByUserId)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.CancelReason = cancelReason;
            result.CancelledByUserId = cancelledByUserId;
        }

        public async Task<IEnumerable<GoodsReceipt>> GetPendingReceiptsAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(g => g.IsActive && g.Status == GoodsReceiptStatus.Pending)
                .OrderByDescending(x => x.ReceiptDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<GoodsReceipt>> GetReceiptsByDateAsync(DateTime fromDate, DateTime toDate)
        {
            var start = fromDate.Date;
            var end = toDate.Date.AddDays(1).AddTicks(-1); 

            return await _dbSet.AsNoTracking()
                .Where(g => g.IsActive && g.ReceiptDate >= start && g.ReceiptDate <= end)
                .OrderByDescending(g => g.ReceiptDate)
                .ToListAsync();
        }
        public async Task<GoodsReceipt?> GetReceiptWithItemsAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Include(g => g.GoodsReceiptItems)
                .FirstOrDefaultAsync(g => g.Id == id && g.IsActive);
        }
    }
}