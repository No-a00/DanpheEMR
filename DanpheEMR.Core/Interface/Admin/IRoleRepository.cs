using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Admin
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
     
        // 1. Kiểm tra xem Tên Vai trò đã tồn tại chưa (Tránh tạo 2 role "Admin")
        Task<bool> IsRoleNameExistsAsync(string roleName, int? excludeId = null);

        // 2. Tìm Role theo Tên (Dùng khi code cứng một số logic, VD: GetByName("Doctor"))
        Task<Role> GetByNameAsync(string roleName);

        // 3. QUAN TRỌNG NHẤT: Lấy thông tin Role KÈM THEO toàn bộ danh sách Quyền của nó
        // (Sử dụng .Include(r => r.RolePermissions).ThenInclude(rp => rp.Permission) trong EF Core)
        Task<Role> GetRoleWithPermissionsAsync(int roleId);

        // 4. Lấy thông tin Role KÈM THEO danh sách User đang giữ Role đó
        // (Dùng để hiển thị: "Vai trò Kế toán hiện đang có 5 nhân viên nắm giữ")
        Task<Role> GetRoleWithUsersAsync(int roleId);
    }
}