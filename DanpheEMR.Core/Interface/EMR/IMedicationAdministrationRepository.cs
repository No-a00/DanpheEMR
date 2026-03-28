using DanpheEMR.Core.Domain.EMR;

namespace DanpheEMR.Core.Interface.EMR
{
    public interface IMedicationAdministrationRepository
    {
      
        Task<MedicationAdministration> GetByIdAsync(Guid Id);
        Task<MedicationAdministration> AddAsync(MedicationAdministration administration);
        Task UpdateAsync(MedicationAdministration administration);
        // Tuyệt đối không có hàm Delete. Nếu y tá nhập sai, chỉ được phép Hủy và ghi rõ lý do.
        Task VoidAdministrationAsync(Guid Id, string voidReason, Guid voidedByUserId);
        // Lấy toàn bộ lịch sử dùng thuốc của một bệnh nhân trong lần nhập viện này
        Task<IEnumerable<MedicationAdministration>> GetByAdmissionIdAsync(Guid admissionId);
        // Kiểm tra xem một loại thuốc cụ thể (PrescriptionItemId) đã được cho uống/tiêm mấy lần rồi
        Task<IEnumerable<MedicationAdministration>> GetByPrescriptionItemIdAsync(Guid prescriptionItemId);
        // Xem danh sách các loại thuốc mà 1 Y tá cụ thể đã thực hiện trong 1 ngày/ca trực
        Task<IEnumerable<MedicationAdministration>> GetByNurseIdAsync(Guid nurseId, DateTime date);
    }
}