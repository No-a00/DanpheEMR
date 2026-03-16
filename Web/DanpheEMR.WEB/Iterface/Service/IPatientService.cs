using DanpheEMR.WEB.Iterface.Repository;
using DanpheEMR.WEB.Models;
namespace DanpheEMR.WEB.Iterface.Service
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientModel>> GetAllPatientsAsync();
        Task<PatientModel> GetPatientByIdAsync(int patientId);
        Task<PatientModel> CreatePatientAsync(PatientModel patient);
        Task UpdatePatientInfoAsync(PatientModel patient);
        Task DeletePatientAsync(int patientId);
        Task ArchivePatientAsync(int patientId);
        Task AdmitPatientAsync(int patientId, int departmentId);
        Task DischargePatientAsync(int patientId);
        Task AssignDoctorToPatientAsync(int patientId, int doctorId);
        Task TransferPatientAsync(int patientId, int fromDepartmentId, int toDepartmentId);
        Task UpdatePatientStatusAsync(int patientId, PatientStatus newStatus);
        Task<IEnumerable<PatientModel>> SearchPatientsAsync(string keyword);
        Task<IEnumerable<PatientModel>> GetPatientsByDoctorAsync(int doctorId);
        Task<IEnumerable<PatientModel>> GetActivePatientsAsync();
        //Task<PatientHistoryDto> GetPatientMedicalHistoryAsync(int patientId);
    }
}
