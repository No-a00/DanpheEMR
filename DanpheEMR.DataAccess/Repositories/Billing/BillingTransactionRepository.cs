using DanpheEMR.Core.Domain.Billing;
using DanpheEMR.Core.Interface.Billing;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Billing
{
    public class BillingTransactionRepository : IBillingTransactionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<BillingTransaction> _dbSet;

        public BillingTransactionRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<BillingTransaction>();
        }

        public async Task AddAsync(BillingTransaction transaction)
        {
            await _dbSet.AddAsync(transaction);
        }

        public void Update(BillingTransaction transaction)
        {
            _dbSet.Update(transaction);
        }

        public async Task<BillingTransaction?> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BillingTransaction?> GetTransactionWithDetailsAsync(int id)
        {
            return await _dbSet.AsNoTracking()
                .Include(b => b.Patient)
                .Include(a => a.Visit)
                .Include(a => a.TransactionItems)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CancelTransactionAsync(int id, string cancelReason)
        {
            var result = await _dbSet.FindAsync(id);
            if (result != null)
            {
                result.isActive = false;
                result.cancelReason = cancelReason;
            }
        }

        public async Task<IEnumerable<BillingTransaction>> SearchTransactionsAsync(BillingSearchFilter filter, int pageNumber, int pageSize)
        {
            IQueryable<BillingTransaction> query = _dbSet.AsNoTracking();

            if (filter.PatientId.HasValue)
            {
                query = query.Where(x => x.PatientId == filter.PatientId.Value);
            }
            if (filter.VisitId.HasValue)
            {
                query = query.Where(x => x.VisitId == filter.VisitId.Value);
            }
            if (!string.IsNullOrWhiteSpace(filter.PaymentStatus))
            {
                query = query.Where(x => x.PaymentStatus == filter.PaymentStatus);
            }
            if (filter.FromDate.HasValue)
            {
                query = query.Where(x => x.TransactionDate >= filter.FromDate.Value);
            }
            if (filter.ToDate.HasValue)
            {
                var endOfDay = filter.ToDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(x => x.TransactionDate <= endOfDay);
            }

            query = query.OrderByDescending(x => x.CreatedAt);
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // 5. HOÀN THIỆN HÀM TÍNH DOANH THU THEO BÁC SĨ
        public async Task<decimal> CalculateTotalRevenueByProviderAsync(int providerId, DateTime fromDate, DateTime toDate)
        {
            var endOfDay = toDate.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()

                .Where(b => b.TransactionDate >= fromDate
                         && b.TransactionDate <= endOfDay
                         && b.isActive == true)
                .SelectMany(b => b.TransactionItems)
                .Where(item => item.ProviderId == providerId)
                .SumAsync(item => item.TotalAmount );
        }
    }
}