using DanpheEMR.Core.Domain.Billing;
using DanpheEMR.Core.Interface.Billing;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Billing
{
    public class ReceiptRepository : GenericRepository<Receipt>,IReceiptRepository
    {
  

        public ReceiptRepository(ApplicationDbContext context) : base(context) { }
        public async Task<Receipt?> GetByReceiptNumberAsync(string receiptNumber)
        {

            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(r => r.ReceiptNumber == receiptNumber);
        }

        public async Task<IEnumerable<Receipt>> GetReceiptsByTransactionIdAsync(Guid transactionId)   
        {
            return await _dbSet.AsNoTracking()
                .Where(r => r.BillingtransactionId == transactionId)
                .ToListAsync();
        }
    }
}