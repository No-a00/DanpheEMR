using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanpheEMR.Core.Interface.Admin
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IEnumerable<User>> GetUsersWithRolesAsync();
        Task<User> GetByUsernameAsync(string username);
        Task <IEnumerable<Permission>> GetPermissionsByUserIdAsync(Guid userId);
        Task<bool> CheckUserPermissionAsync(Guid userId, string action, string resource);
        Task<User> GetUserWithEmployeeDetailsAsync(Guid userId);

        Task<bool> IsUsernameUniqueAsync(string username);
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> UserHasRoleAsync(Guid userId, Guid roleId);
        Task AddUserRoleAsync(UserRole userRole);



        Task<UserRole> GetUserRoleAsync(Guid userId, Guid roleId);
        void RemoveUserRole(UserRole userRole);
        //token
        Task<User> GetByRefreshTokenAsync(string refreshToken);
    }
}