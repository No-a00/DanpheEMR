using DanpheEMR.Core.Domain.Admin;

namespace DanpheEMR.Core.Interface.Admin
{
    public interface IUserRepository
    {
        
        Task<User> GetByIdAsync(Guid Id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);

        // Khóa tài khoản thay vì xóa
        Task DeactivateUserAsync(Guid Id);

        // Nghiệp vụ đăng nhập & phân quyền
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetUserWithRolesAndPermissionsAsync(Guid userId);
        Task<bool> CheckUserPermissionAsync(Guid userId, string action, string resource);
        Task<User> GetUserWithEmployeeDetailsAsync(Guid userId);
    }
}