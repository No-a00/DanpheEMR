using DanpheEMR.Core.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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


        Task<User> GetByUsernameAsync(string username);
        Task<User> GetUserWithRolesAndPermissionsAsync(Guid userId);
        Task<bool> CheckUserPermissionAsync(Guid userId, string action, string resource);
        Task<User> GetUserWithEmployeeDetailsAsync(Guid userId);

        Task<bool> IsUsernameUniqueAsync(string username);
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> UserHasRoleAsync(Guid userId, Guid roleId);
        Task AddUserRoleAsync(UserRole userRole);
    }
}