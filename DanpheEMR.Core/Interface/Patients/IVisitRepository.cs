using DanpheEMR.Core.Domain.Patients;

namespace DanpheEMR.Core.Interface.Patients
{
    public interface IVisitRepository
    {
        Task<Visit> GetByIdAsync(Guid Id);
        Task<Visit> AddAsync(Visit visit);
        Task UpdateAsync(Visit visit);
        Task CancelVisitAsync(Guid Id, string cancelReason,int userIdCancel);
        Task<IEnumerable<Visit>> GetActiveVisitsByProviderAsync(int providerId, DateTime date);
        Task<IEnumerable<Visit>> GetVisitsByPatientIdAsync(Guid patientId);
        // Dùng khi Bác sĩ bấm nút "In Hồ Sơ Bệnh Án" hoặc "In Sổ Khám Bệnh"
        Task<Visit> GetVisitWithAllDetailsAsync(Guid Id);
    }
}