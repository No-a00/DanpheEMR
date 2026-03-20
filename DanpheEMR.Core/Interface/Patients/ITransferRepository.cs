using DanpheEMR.Core.Domain.Patients;

namespace DanpheEMR.Core.Interface.Patients
{
    public interface ITransferRepository
    {
        Task<Transfer> GetByIdAsync(int id);
        Task<Transfer> AddAsync(Transfer transfer);
        Task UpdateAsync(Transfer transfer);
        Task CancelTransferAsync(int id, string cancelReason,int voidedByUserId);
        // Xem lịch sử luân chuyển của 1 bệnh nhân trong đợt nằm viện này 
        Task<IEnumerable<Transfer>> GetTransfersByAdmissionIdAsync(int admissionId);
        // Lọc danh sách bệnh nhân ĐANG CHỜ CHUYỂN ĐẾN khoa của mình (để sắp xếp giường trống)
        Task<IEnumerable<Transfer>> GetPendingTransfersToDepartmentAsync(int toDeptId);
    }
}