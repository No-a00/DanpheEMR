using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Base;


namespace DanpheEMR.Core.Interface.Patients
{
    
    public interface IAdmissionRepository : IGenericRepository<Admission>
    {
        Task<IEnumerable<Admission>> GetActiveAdmissionsAsync();
        Task<IEnumerable<Admission>> GetAdmissionsByPatientIdAsync(Guid patientId);
        Task<Admission> GetAdmissionWithTransfersAsync(Guid admissionId);
    }
}