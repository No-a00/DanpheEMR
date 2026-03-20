
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Admin
{
    public class RoleRepository :  GenericRepository<Role>,IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<bool> IsRoleNameExistsAsync(string roleName, int? excludeId = null) { 
            return await _dbSet.AnyAsync(r => r.RoleName == roleName && (!excludeId.HasValue || r.Id != excludeId.Value));
        }
        public async Task<Role> GetByNameAsync(string roleName) { 
               return await _dbSet.FirstOrDefaultAsync(r => r.RoleName == roleName);
        }
        public async Task<Role> GetRoleWithPermissionsAsync(int roleId) { 
        return  await _dbSet.Include(r => r.RolePermissions)
                     .ThenInclude(rp => rp.Permission)
                     .FirstOrDefaultAsync(r => r.Id == roleId);
        }
        public async Task<Role> GetRoleWithUsersAsync(int roleId) {
            return await _dbSet.Include(r => r.UserRoles)
                     .ThenInclude(ur => ur.User)
                     .FirstOrDefaultAsync(r => r.Id == roleId);
        }

    }
}
