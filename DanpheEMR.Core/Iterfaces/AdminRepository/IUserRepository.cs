using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Iterface.Base;

namespace DanpheEMR.Core.Iterfaces.IAdminRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        // Quản trị viên khóa tài khoản (Thay vì xóa vật lý)
        Task DeactivateUserAsync(int userId);
        // 1. Tải User KÈM THEO toàn bộ Vai trò (Roles) và Quyền hạn (Permissions) của họ
        Task<User> GetUserWithRolesAndPermissionsAsync(int userId);
        // 2. Kiểm tra trực tiếp: User A có quyền làm Hành động B trên Tài nguyên C không?
        Task<bool> CheckUserPermissionAsync(int userId, string action, string resource);
        // 3. Lấy thông tin Tài khoản KÈM THEO hồ sơ nhân sự (Employee)
        Task<User> GetUserWithEmployeeDetailsAsync(int userId);
    }
}
