using DanpheEMR.Core.Domain.EMR;

namespace DanpheEMR.Core.Interface.EMR
{
    public interface IMedicationAdministrationRepository
    {
      
        Task<MedicationAdministration> GetByIdAsync(int id);
        Task<MedicationAdministration> AddAsync(MedicationAdministration administration);
        Task UpdateAsync(MedicationAdministration administration);
        // Tuyệt đối không có hàm Delete. Nếu y tá nhập sai, chỉ được phép Hủy và ghi rõ lý do.
        Task VoidAdministrationAsync(int id, string voidReason, int voidedByUserId);
        // Lấy toàn bộ lịch sử dùng thuốc của một bệnh nhân trong lần nhập viện này
        Task<IEnumerable<MedicationAdministration>> GetByAdmissionIdAsync(int admissionId);
        // Kiểm tra xem một loại thuốc cụ thể (PrescriptionItemId) đã được cho uống/tiêm mấy lần rồi
        Task<IEnumerable<MedicationAdministration>> GetByPrescriptionItemIdAsync(int prescriptionItemId);
        // Xem danh sách các loại thuốc mà 1 Y tá cụ thể đã thực hiện trong 1 ngày/ca trực
        Task<IEnumerable<MedicationAdministration>> GetByNurseIdAsync(int nurseId, DateTime date);
    }
}