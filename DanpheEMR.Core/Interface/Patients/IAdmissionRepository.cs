using DanpheEMR.Core.Domain.Patients;

namespace DanpheEMR.Core.Interface.Patients
{
    public interface IAdmissionRepository
    {
        Task<Admission> GetByIdAsync(Guid Id);
        Task<Admission> AddAsync(Admission admission);
        Task UpdateAsync(Admission admission);
        Task<IEnumerable<Admission>> GetActiveAdmissionsAsync();

        Task<IEnumerable<Admission>> GetAdmissionsByPatientIdAsync(Guid patientId);
        Task<Admission> GetAdmissionWithTransfersAsync(Guid admissionId);
    }
}