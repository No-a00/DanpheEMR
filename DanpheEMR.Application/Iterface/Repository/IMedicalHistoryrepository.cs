using DanpheEMR.WEB.Models;

namespace DanpheEMR.WEB.Iterface.Repository
{
    public interface IMedicalHistoryRepository: IGenericRepository<MedicalHistoryModel>
    {
        public Task<List<MedicalHistoryModel>> GetMedicalHistoryByPatientIdAsync(int patientId);
        public Task<List<MedicalHistoryModel>> GetMedicalHistoryByDateAsync(DateTime date);
        public Task<List<MedicalHistoryModel>> GetMedicalHistoryByDoctorIdAsync(int doctorId);
        public Task<List<MedicalHistoryModel>> GetMedicalHistoryBySymptomsAsync(string symptoms);

    }
}