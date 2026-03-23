using DanpheEMR.Core.Domain.Billing;
using DanpheEMR.Core.Interface.Billing;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Billing
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly DbSet<Receipt> _dbSet;

        public ReceiptRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Receipt>(); 
        }

        public async Task<Receipt?> GetByIdAsync(Guid id)
        {
        
            return await _dbSet.FindAsync(id);
        }

        public async Task<Receipt> AddAsync(Receipt receipt)
        {
            await _dbSet.AddAsync(receipt);
            return receipt; 
        }

        public async Task CancelReceiptAsync(int receiptId, string reason,int cancelUserId)
        {
            var receipt = await _dbSet.FindAsync(receiptId);
            if (receipt == null && receipt.IsActive == false) return;
            receipt.IsActive = false;
            receipt.CancelReason = reason;
            receipt.CancelUserId = cancelUserId;
        }

        public async Task<Receipt?> GetByReceiptNumberAsync(string receiptNumber)
        {
            // Dùng AsNoTracking cho thao tác chỉ đọc
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(r => r.ReceiptNumber == receiptNumber);
        }

        public async Task<IEnumerable<Receipt>> GetReceiptsByTransactionIdAsync(int transactionId)   
        {
            // Tìm tất cả các biên lai thuộc về 1 mã hóa đơn (BillingTransaction)
            return await _dbSet.AsNoTracking()
                .Where(r => r.BillingtransactionId == transactionId)
                .ToListAsync();
        }
    }
}