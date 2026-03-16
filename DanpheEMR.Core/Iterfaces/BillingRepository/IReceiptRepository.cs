using DanpheEMR.Core.Domain.Appointment;
using DanpheEMR.Core.Domain.Nums;
using DanpheEMR.Core.Iterface.Base;

namespace DanpheEMR.Core.Iterfaces.BillingRepository
{
    public interface IReceiptRepository : IGenericRepository<Receipt>
    {
        Task<Receipt> GetByReceiptNumberAsync(string receiptNumber);

        // Hủy biên lai (kèm lý do)
        Task CancelReceiptAsync(int receiptId, string reason);

        // Báo cáo chốt ca cuối ngày: Tính tổng tiền thu được theo từng Hình thức thanh toán (Tiền mặt, Chuyển khoản...)
        Task<decimal> GetTotalCollectionByPaymentModeAsync(PaymentStatus mode, DateTime fromDate, DateTime toDate);
    }
}
