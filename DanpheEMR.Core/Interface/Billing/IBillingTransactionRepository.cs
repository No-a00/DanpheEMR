using DanpheEMR.Core.Domain.Billing;
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Billing
{
    public class BillingSearchFilter
    {
        public Guid? PatientId { get; set; }
        public Guid? VisitId { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
    public interface IBillingTransactionRepository : IGenericRepository<BillingTransaction>
    {

        // Lấy hóa đơn KÈM THEO danh sách chi tiết (Rất hay dùng khi In hóa đơn)
        Task<BillingTransaction?> GetTransactionWithDetailsAsync(Guid Id);

        // Bắt buộc phải tìm kiếm có điều kiện (Filter) để tránh tải hàng triệu dòng
        //Lấy doanh thu theo ngày
        Task<IEnumerable<BillingTransaction>> GetPaidTransactionsByDateAsync(DateTime reportDate);

        Task<IEnumerable<BillingTransaction>> SearchTransactionsAsync(BillingSearchFilter filter, int pageNumber, int pageSize);

        // Báo cáo doanh thu theo Bác sĩ
        Task<decimal> CalculateTotalRevenueByProviderAsync(Guid providerId, DateTime fromDate, DateTime toDate);
        //
        Task<IEnumerable<BillingTransaction>> GetUnpaidTransactionsByPatientAsync(Guid patientId);
    }
}