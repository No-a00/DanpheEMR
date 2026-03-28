using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Admin
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        // 1. Lấy danh sách các Khoa Khám Bệnh (Lâm sàng) - Phục vụ luồng Đặt lịch khám
        Task<IEnumerable<Department>> GetClinicalDepartmentsAsync();

        // 2. Lấy tất cả các Khoa Gốc (Khoa cha cấp cao nhất)
        Task<IEnumerable<Department>> GetRootDepartmentsAsync();

        // 3. Lấy danh sách các Khoa con trực thuộc một Khoa cha
        Task<IEnumerable<Department>> GetSubDepartmentsAsync(Guid parentDepartmentId);

        // 4. Lấy thông tin một Khoa KÈM THEO danh sách Bác sĩ/Nhân viên
        Task<Department> GetDepartmentWithEmployeesAsync(Guid departmentId);

        // 5. Kiểm tra Mã khoa đã tồn tại chưa (Dùng IsCodeExistsAsync cho gọn)
        Task<bool> IsCodeExistsAsync(string departmentCode, Guid? excludeId = null);
    }
}