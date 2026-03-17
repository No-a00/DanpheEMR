using DanpheEMR.Core.Domain.Patients;
namespace DanpheEMR.Core.Interface.Patients
{
    public interface IDischargeRepository
    {
        Task<Discharge> GetByIdAsync(int id);
        Task<Discharge> AddAsync(Discharge discharge);
        Task UpdateAsync(Discharge discharge);
        Task VoidDischargeAsync(int id, string voidReason, int voidedByUserId);

        //  Lấy Giấy ra viện của một đợt nằm viện cụ thể 
        Task<Discharge> GetByAdmissionIdAsync(int admissionId);

        // Lấy lịch sử các lần ra viện của bệnh nhân (kèm theo tình trạng lúc ra viện)
        Task<IEnumerable<Discharge>> GetDischargesByPatientIdAsync(int patientId);

        // Thống kê: Lấy danh sách toàn bộ bệnh nhân đã xuất viện trong ngày hôm nay
        Task<IEnumerable<Discharge>> GetDischargesByDateAsync(DateTime date);

        // Lọc theo Tình trạng (Ví dụ: Giám đốc muốn xem tháng này có bao nhiêu ca xuất viện với tình trạng "Tử vong" hoặc "Nặng xin về")
        Task<IEnumerable<Discharge>> GetDischargesByConditionAsync(string condition, DateTime fromDate, DateTime toDate);
    }
}