using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Appointment
{
    public class BillingSearchFilter
    {
        public int? PatientId { get; set; }
        public int? VisitId { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public interface IBillingTransactionRepository
    {
        // Thay cho hàm Delete: Đánh dấu hóa đơn bị hủy và ghi lại lý do
        Task CancelTransactionAsync(int id, string cancelReason);

        // --- Nhóm 2: Truy vấn & Báo cáo ---

        // Hàm tìm kiếm tổng hợp dùng cho màn hình Quản lý thu ngân
        Task<IEnumerable<BillingTransaction>> SearchTransactionsAsync(BillingSearchFilter filter);

        // Báo cáo doanh thu theo Bác sĩ (Dựa vào ProviderId ở tầng TransactionItem)
        Task<decimal> CalculateTotalRevenueByProviderAsync(int providerId, DateTime fromDate, DateTime toDate);
    }
}