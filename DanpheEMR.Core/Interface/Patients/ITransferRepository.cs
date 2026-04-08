using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Patients
{
    public interface ITransferRepository : IGenericRepository<Transfer>
    {
        // Xem lịch sử luân chuyển của 1 bệnh nhân trong đợt nằm viện này 
        Task<IEnumerable<Transfer>> GetTransfersByAdmissionIdAsync(Guid admissionId);
        // Lọc danh sách bệnh nhân ĐANG CHỜ CHUYỂN ĐẾN khoa của mình (để sắp xếp giường trống)
        Task<IEnumerable<Transfer>> GetPendingTransfersToDepartmentAsync(Guid toDeptId);
    }
}