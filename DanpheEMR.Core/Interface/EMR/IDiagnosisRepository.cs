using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.Base;
namespace DanpheEMR.Core.Interface.EMR
{
    public interface IDiagnosisRepository : IGenericRepository<Diagnosis>
    {
        // Lấy theo lượt khám (1 lần khám cụ thể)
        Task<IEnumerable<Diagnosis>> GetDiagnosesByVisitIdAsync(Guid visitId);

        // BỔ SUNG: Lấy toàn bộ lịch sử chẩn đoán của 1 bệnh nhân (Dùng cho GetPatientMedicalHistory)
        Task<IEnumerable<Diagnosis>> GetDiagnosesByPatientIdAsync(Guid patientId);

        // Thống kê theo mã bệnh ICD-10
        Task<IEnumerable<Diagnosis>> GetDiagnosesByICD10Async(string icd10Code, DateTime fromDate, DateTime toDate);
    }
}