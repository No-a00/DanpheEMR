using DanpheEMR.Core.Domain.Billing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanpheEMR.Core.Interface.Billing
{
    public interface IReceiptRepository
    {
        Task<Receipt> GetByIdAsync(Guid Id);
        Task<Receipt> AddAsync(Receipt receipt);

        Task CancelReceiptAsync(Guid receiptId, string reason, Guid cancelUserId);

        Task<Receipt> GetByReceiptNumberAsync(string receiptNumber);

     
        Task<IEnumerable<Receipt>> GetReceiptsByTransactionIdAsync(Guid transactionId);
    }
}