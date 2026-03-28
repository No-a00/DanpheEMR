using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Base; // Nhớ using IGenericRepository


namespace DanpheEMR.Core.Interface.Patients
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<Patient> GetByPatientCodeAsync(string patientCode);

        Task<IEnumerable<Patient>> SearchPatientsAsync(string keyword);

        Task<Patient> GetPatientWithDetailsAsync(Guid id);

        Task<bool> IsIdCardExistsAsync(string idCardNumber);

        Task<string> GeneratePatientCodeAsync();
    }
}