using DanpheEMR.Core.Domain.Appointment;

namespace DanpheEMR.Core.Domain.Billing
{
    public interface IReceiptRepository
    {
        Task<Receipt> GetByIdAsync(int id);
        Task<Receipt> AddAsync(Receipt receipt);

        // Biên lai in ra rồi không được phép Sửa (Update) hay Xóa (Delete). Chỉ được phép Hủy (Cancel).
        Task CancelReceiptAsync(int receiptId, string reason);

        // Truy xuất
        Task<Receipt> GetByReceiptNumberAsync(string receiptNumber);
        Task<IEnumerable<Receipt>> GetReceiptsByTransactionIdAsync(int transactionId);
    }
}