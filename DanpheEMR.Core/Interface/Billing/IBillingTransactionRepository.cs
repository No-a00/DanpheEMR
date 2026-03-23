using DanpheEMR.Core.Domain.Billing;

namespace DanpheEMR.Core.Interface.Billing
{
    public class BillingSearchFilter
    {
        public int? PatientId { get; set; }
        public int? VisitId { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
    public interface IBillingTransactionRepository
    {
        Task AddAsync(BillingTransaction transaction);

        // Cập nhật hóa đơn (Chỉ nên dùng khi hóa đơn đang ở trạng thái "Nháp" - Draft)
        void Update(BillingTransaction transaction);
        Task<BillingTransaction?> GetByIdAsync(Guid Id);

        // Lấy hóa đơn KÈM THEO danh sách chi tiết (Rất hay dùng khi In hóa đơn)
        Task<BillingTransaction?> GetTransactionWithDetailsAsync(Guid Id);
        // Tuyệt đối KHÔNG có hàm Delete. Thay vào đó là hàm Hủy hóa đơn.
        Task CancelTransactionAsync(Guid Id, string cancelReason,int cancelUserId);
        // Bắt buộc phải tìm kiếm có điều kiện (Filter) để tránh tải hàng triệu dòng
        Task<IEnumerable<BillingTransaction>> SearchTransactionsAsync(BillingSearchFilter filter, int pageNumber, int pageSize);

        // Báo cáo doanh thu theo Bác sĩ
        Task<decimal> CalculateTotalRevenueByProviderAsync(int providerId, DateTime fromDate, DateTime toDate);
    }
}