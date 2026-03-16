using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Iterface.Base;

namespace DanpheEMR.Core.Iterfaces.IAdminRepository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        // 1. Tìm bác sĩ/nhân viên theo Tên hoặc Số điện thoại
        Task<IEnumerable<Employee>> SearchByNameOrContactAsync(string keyword);

        // 2. Lấy danh sách toàn bộ Bác sĩ/Nhân viên thuộc MỘT KHOA cụ thể
        // (Rất cần thiết khi Giám đốc muốn xem danh sách nhân sự của Khoa Nội)
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int departmentId);

        // --- NHÓM 3: Giao tiếp với Hệ thống Tài khoản (Authentication) ---

        // 3. Lấy thông tin Nhân sự dựa vào ID của Tài khoản đăng nhập (UserId)
        // (Dùng khi Bác sĩ login thành công, hệ thống cần biết họ tên, khoa phòng của họ để hiển thị lên góc màn hình)
        Task<Employee> GetEmployeeByUserIdAsync(int userId);

        // 4. Lấy chi tiết Nhân viên KÈM THEO thông tin Khoa (Department) và Tài khoản (Users)
        Task<Employee> GetEmployeeWithDetailsAsync(int id);
    }
}
