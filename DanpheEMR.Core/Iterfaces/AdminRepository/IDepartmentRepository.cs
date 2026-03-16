using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Iterface.Base;

namespace DanpheEMR.Core.Iterfaces.IAdminRepository
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        // 1. Lấy danh sách các Khoa Khám Bệnh (Lâm sàng) - Phục vụ luồng Đặt lịch khám
        Task<IEnumerable<Department>> GetClinicalDepartmentsAsync();

        // 2. Lấy tất cả các Khoa Gốc (Khoa cha cấp cao nhất, ParentDepartmentId == null)
        Task<IEnumerable<Department>> GetRootDepartmentsAsync();

        // 3. Lấy danh sách các Khoa con trực thuộc một Khoa cha
        // VD: Truyền Id của Khoa Nội -> Trả về Nội Tim Mạch, Nội Tiêu Hóa
        Task<IEnumerable<Department>> GetSubDepartmentsAsync(int parentDepartmentId);

        // --- NHÓM 3: Tải dữ liệu kèm theo (Eager Loading) ---

        // 4. Lấy thông tin một Khoa KÈM THEO danh sách Bác sĩ đang làm việc ở đó
        // (Sử dụng .Include(x => x.Employees) trong EF Core)
        Task<Department> GetDepartmentWithEmployeesAsync(int departmentId);

        // 5. Kiểm tra Mã khoa (DepartmentCode) đã tồn tại chưa để tránh tạo trùng (VD: "KKB", "KNOI")
        Task<bool> IsCodeExistsAsync(string departmentCode, int? excludeId = null);

    }
}
