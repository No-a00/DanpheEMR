using DanpheEMR.Core.Domain.Billing;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Billing
{
    public interface IReceiptRepository : IGenericRepository<Receipt>
    {
        Task<Receipt> GetByReceiptNumberAsync(string receiptNumber);
        Task<IEnumerable<Receipt>> GetReceiptsByTransactionIdAsync(Guid transactionId);
    }
}