using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Admin
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
     
        //  Kiểm tra xem Tên Vai trò đã tồn tại chưa (Tránh tạo 2 role "Admin")
        Task<bool> IsRoleNameExistsAsync(string roleName, Guid? excludeId = null);
        Task<Role> GetRoleWithPermissionsAsync(Guid roleId);

        // 4. Lấy thông tin Role KÈM THEO danh sách User đang giữ Role đó
        Task<Role> GetRoleWithUsersAsync(Guid roleId);
    }
}