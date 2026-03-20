using DanpheEMR.Core.Domain.Patients;

namespace DanpheEMR.Core.Interface.Patients
{
    public interface IVisitRepository
    {
        Task<Visit> GetByIdAsync(int id);
        Task<Visit> AddAsync(Visit visit);
        Task UpdateAsync(Visit visit);
        Task CancelVisitAsync(int id, string cancelReason);
        Task<IEnumerable<Visit>> GetActiveVisitsByProviderAsync(int providerId, DateTime date);
        Task<IEnumerable<Visit>> GetVisitsByPatientIdAsync(int patientId);
        // Dùng khi Bác sĩ bấm nút "In Hồ Sơ Bệnh Án" hoặc "In Sổ Khám Bệnh"
        Task<Visit> GetVisitWithAllDetailsAsync(int id);
    }
}