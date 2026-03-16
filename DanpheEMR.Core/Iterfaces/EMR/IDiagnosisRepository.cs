using DanpheEMR.Core.Domain.Appointment;

namespace DanpheEMR.Core.Domain.EMR
{
    public interface IDiagnosisRepository
    {
        Task<Diagnosis> GetByIdAsync(int id);
        Task<Diagnosis> AddAsync(Diagnosis diagnosis);
        Task UpdateAsync(Diagnosis diagnosis);

        // Hủy chẩn đoán 
        Task VoidDiagnosisAsync(int diagnosisId, string reason);

        // Lấy theo lượt khám hoặc thống kê theo mã bệnh
        Task<IEnumerable<Diagnosis>> GetDiagnosesByVisitIdAsync(int visitId);
        Task<IEnumerable<Diagnosis>> GetDiagnosesByICD10Async(string icd10Code, DateTime fromDate, DateTime toDate);
    }
}