using DanpheEMR.Core.Domain.Patients;
namespace DanpheEMR.Core.Interface.Patients
{
    public interface IDischargeRepository
    {
        Task<Discharge> GetByIdAsync(Guid Id);
        Task<Discharge> AddAsync(Discharge discharge);
        Task UpdateAsync(Discharge discharge);
        Task VoidDischargeAsync(Guid Id, string voidReason, Guid voidedByUserId);

        //  Lấy Giấy ra viện của một đợt nằm viện cụ thể 
        Task<Discharge> GetByAdmissionIdAsync(Guid admissionId);

        // Lấy lịch sử các lần ra viện của bệnh nhân (kèm theo tình trạng lúc ra viện)
        Task<IEnumerable<Discharge>> GetDischargesByPatientIdAsync(Guid patientId);

        // Thống kê: Lấy danh sách toàn bộ bệnh nhân đã xuất viện trong ngày hôm nay
        Task<IEnumerable<Discharge>> GetDischargesByDateAsync(DateTime date);

        // Lọc theo Tình trạng (Ví dụ: Giám đốc muốn xem tháng này có bao nhiêu ca xuất viện với tình trạng "Tử vong" hoặc "Nặng xin về")
        Task<IEnumerable<Discharge>> GetDischargesByConditionAsync(string condition, DateTime fromDate, DateTime toDate);
    }
}