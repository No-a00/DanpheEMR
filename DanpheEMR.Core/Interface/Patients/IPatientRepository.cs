using DanpheEMR.Core.Domain.Patients;
namespace DanpheEMR.Core.Interface.Patients
{
    public interface IPatientRepository
    {
        Task<Patient> GetByIdAsync(int id);

        // Khi tạo mới, hệ thống sẽ tự động phát sinh PatientCode (VD: "PAT-2026-0001")
        Task<Patient> AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeactivateAsync(int id,string voidReason,int voidedByUserId);
        // Tìm chính xác bằng Mã bệnh nhân (Khi bệnh nhân đưa thẻ cứng hoặc đọc mã)
        Task<Patient> GetByPatientCodeAsync(string patientCode);
        // Tìm kiếm linh hoạt: Gõ Tên, hoặc Số điện thoại, hoặc CMND/CCCD để quét xem người này đã có hồ sơ chưa
        Task<IEnumerable<Patient>> SearchPatientsAsync(string keyword);
        // Lấy thông tin Bệnh nhân KÈM THEO Địa chỉ (Addresses) và Người nhà (Kins)
        Task<Patient> GetPatientWithDetailsAsync(int id);
    }
}