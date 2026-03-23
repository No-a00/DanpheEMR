using DanpheEMR.Core.Domain.EMR;
namespace DanpheEMR.Core.Interface.EMR
{
    public interface IVitalsRepository
    {
        
        Task<Vitals> GetByIdAsync(Guid Id);
        Task<Vitals> AddAsync(Vitals vitals);
        Task UpdateAsync(Vitals vitals);

        // Sinh hiệu đo sai thì đánh dấu Hủy (Không xóa vật lý)
        Task VoidVitalsAsync(Guid Id, string voidReason, int voidedByUserId);

        // Lấy danh sách các lần đo sinh hiệu CỦA MỘT LƯỢT KHÁM
        // (Thường thì 1 lượt khám chỉ đo 1 lần, nhưng nếu bệnh nhân cấp cứu nằm theo dõi lâu, có thể đo mỗi 30 phút/lần)
        Task<IEnumerable<Vitals>> GetByVisitIdAsync(int visitId);

        // Lấy toàn bộ lịch sử sinh hiệu của Bệnh nhân qua các năm
        // (Dùng để vẽ biểu đồ Huyết áp, nhịp tim, hoặc theo dõi tiến trình giảm cân/BMI)
        Task<IEnumerable<Vitals>> GetHistoryByPatientIdAsync(Guid patientId);

        // Lấy nhanh chỉ số Sinh hiệu MỚI NHẤT của bệnh nhân
        // (Dùng để hiển thị nhấp nháy trên màn hình tổng quan hồ sơ)
        Task<Vitals> GetLatestVitalsByPatientIdAsync(Guid patientId);
    }
}