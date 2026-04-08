using DanpheEMR.Core.Domain.Billing;
using DanpheEMR.Core.Interface.Billing;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using DanpheEMR.Core.Enums;
using DanpheEMR.DataAccess.Repositories.Base;

namespace DanpheEMR.DataAccess.Repositories.Billing
{
    public class BillingTransactionRepository : GenericRepository<BillingTransaction>,IBillingTransactionRepository
    {


        public BillingTransactionRepository(ApplicationDbContext context) : base(context)   { }

        public async Task<BillingTransaction?> GetTransactionWithDetailsAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Include(b => b.Patient)
                .Include(a => a.Visit)
                .Include(a => a.TransactionItems)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

       
        public async Task<IEnumerable<BillingTransaction>> GetPaidTransactionsByDateAsync(DateTime reportDate)
        {
            return await _dbSet.AsNoTracking()
                .Include(b => b.Patient) 
                .Where(b => b.TransactionDate.Date == reportDate.Date
                         && b.PaymentStatus == PaymentStatus.Paid 
                         && !b.IsDeleted)
                .ToListAsync();
        }
        public async Task<IEnumerable<BillingTransaction>> SearchTransactionsAsync(BillingSearchFilter filter, int pageNumber, int pageSize)
        {
            IQueryable<BillingTransaction> query = _dbSet.AsNoTracking();

            if (filter.PatientId.HasValue)
            {
                query = query.Where(x => x.PatientId == filter.PatientId.Value&&!x.IsDeleted);
            }
            if (filter.VisitId.HasValue)
            {
                query = query.Where(x => x.VisitId == filter.VisitId.Value && !x.IsDeleted);
            }
            if (filter.PaymentStatus.HasValue)
            {
                query = query.Where(x => x.PaymentStatus == filter.PaymentStatus.Value && !x.IsDeleted);
            }
            if (filter.FromDate.HasValue)
            {
                query = query.Where(x => x.TransactionDate >= filter.FromDate.Value && !x.IsDeleted);
            }
            if (filter.ToDate.HasValue)
            {
                var endOfDay = filter.ToDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(x => x.TransactionDate <= endOfDay && !x.IsDeleted);
            }

            query = query.OrderByDescending(x => x.CreatedAt);
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<decimal> CalculateTotalRevenueByProviderAsync(Guid providerId, DateTime fromDate, DateTime toDate)
        {
            var endOfDay = toDate.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()

                .Where(b => b.TransactionDate >= fromDate
                         && b.TransactionDate <= endOfDay
                         && !b.IsDeleted)
                .SelectMany(b => b.TransactionItems)
                .Where(item => item.ProviderId == providerId)
                .SumAsync(item => item.TotalAmount );
        }
        
        public async Task<IEnumerable<BillingTransaction>> GetUnpaidTransactionsByPatientAsync(Guid patientId)
        {
            return await _dbSet.AsNoTracking()
                .Where(x => x.PatientId == patientId
                         && x.PaymentStatus == PaymentStatus.Pending 
                         && !x.IsDeleted)
                .ToListAsync();
        }
    }
}