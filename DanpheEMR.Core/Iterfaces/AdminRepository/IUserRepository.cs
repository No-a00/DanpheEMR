using DanpheEMR.Core.Domain.Admin;

namespace DanpheEMR.Core.Domain.Admin
{
    public interface IUserRepository
    {
        
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);

        // Khóa tài khoản thay vì xóa
        Task DeactivateUserAsync(int id);

        // Nghiệp vụ đăng nhập & phân quyền
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetUserWithRolesAndPermissionsAsync(int userId);
        Task<bool> CheckUserPermissionAsync(int userId, string action, string resource);
        Task<User> GetUserWithEmployeeDetailsAsync(int userId);
    }
}