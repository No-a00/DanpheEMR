using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.EMR
{
    public interface IMedicationAdministrationRepository : IGenericRepository<MedicationAdministration>
    {
        // Lấy toàn bộ lịch sử dùng thuốc của một bệnh nhân trong lần nhập viện này
        Task<IEnumerable<MedicationAdministration>> GetByAdmissionIdAsync(Guid admissionId);
        // Kiểm tra xem một loại thuốc cụ thể (PrescriptionItemId) đã được cho uống/tiêm mấy lần rồi
        Task<IEnumerable<MedicationAdministration>> GetByPrescriptionItemIdAsync(Guid prescriptionItemId);
        // Xem danh sách các loại thuốc mà 1 Y tá cụ thể đã thực hiện trong 1 ngày/ca trực
        Task<IEnumerable<MedicationAdministration>> GetByNurseIdAsync(Guid nurseId, DateTime date);
    }
}