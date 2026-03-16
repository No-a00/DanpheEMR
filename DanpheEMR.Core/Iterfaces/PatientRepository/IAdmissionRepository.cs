namespace DanpheEMR.Core.Domain.Patients
{
    public interface IAdmissionRepository
    {
        Task<Admission> GetByIdAsync(int id);
        Task<Admission> AddAsync(Admission admission);
        Task UpdateAsync(Admission admission);
        Task<IEnumerable<Admission>> GetActiveAdmissionsAsync();

        Task<IEnumerable<Admission>> GetAdmissionsByPatientIdAsync(int patientId);
        Task<Admission> GetAdmissionWithTransfersAsync(int admissionId);
    }
}