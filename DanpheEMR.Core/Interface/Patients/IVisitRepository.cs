using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanpheEMR.Core.Interface.Patients
{
    // Kế thừa GenericRepository
    public interface IVisitRepository : IGenericRepository<Visit>
    {
        Task<IEnumerable<Visit>> GetActiveVisitsByProviderAsync(Guid providerId, DateTime date);
        Task<IEnumerable<Visit>> GetVisitsByPatientIdAsync(Guid patientId);

        // Dùng khi Bác sĩ bấm nút "In Hồ Sơ Bệnh Án"
        Task<Visit> GetVisitWithAllDetailsAsync(Guid id);

        // --- BỔ SUNG ---
        Task<string> GenerateVisitCodeAsync();

        // Cấp số thứ tự bốc số theo từng khoa trong một ngày
        Task<int> GenerateQueueNoAsync(Guid departmentId, DateTime date);
    }
}