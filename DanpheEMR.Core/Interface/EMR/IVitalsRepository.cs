using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.Base;
namespace DanpheEMR.Core.Interface.EMR
{
    public interface IVitalsRepository : IGenericRepository<Vitals>
    {
        

        // Lấy danh sách các lần đo sinh hiệu CỦA MỘT LƯỢT KHÁM
       
        Task<IEnumerable<Vitals>> GetByVisitIdAsync(Guid visitId);
        // Lấy toàn bộ lịch sử sinh hiệu của Bệnh nhân qua các năm
        // (Dùng để vẽ biểu đồ Huyết áp, nhịp tim, hoặc theo dõi tiến trình giảm cân/BMI)
        Task<IEnumerable<Vitals>> GetHistoryByPatientIdAsync(Guid patientId);

        // Lấy nhanh chỉ số Sinh hiệu MỚI NHẤT của bệnh nhân
        // (Dùng để hiển thị nhấp nháy trên màn hình tổng quan hồ sơ)
        Task<Vitals> GetLatestVitalsByPatientIdAsync(Guid patientId);
    }
}