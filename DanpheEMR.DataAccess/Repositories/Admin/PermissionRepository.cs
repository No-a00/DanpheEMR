using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;


namespace DanpheEMR.DataAccess.Repositories.Admin
{
    public class PermissionRepository : GenericRepository<Permission>,IPermissionRepository
    {
        public PermissionRepository(ApplicationDbContext context) : base(context) { }
        public async Task<IEnumerable<Permission>> GetPermissionsByResourceAsync(string resource)
        {
            return await _dbSet.AsNoTracking().Where(p => p.Resource == resource).ToListAsync();
        }
        public async Task<bool> HasPermissionAsync(Guid roleId, string action, string resource)
        {
            return await _dbSet.AsNoTracking()
                .AnyAsync(p => p.Action == action && p.Resource == resource && p.RolePermissions.Any(rp => rp.RoleId == roleId));
        }

        public async Task<bool> RoleHasPermissionAsync(Guid roleId, Guid permissionId)
        {
          
            return await _context.Set<RolePermission>()
                .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId && rp.IsActive);
        }

        public async Task AddRolePermissionAsync(RolePermission rolePermission)
        {
            await _context.Set<RolePermission>().AddAsync(rolePermission);
        }

        public async Task<RolePermission?> GetRolePermissionAsync(Guid roleId, Guid permissionId)
        {
            return await _context.Set<RolePermission>()
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
        }

        public void RemoveRolePermission(RolePermission rolePermission)
        {
            _context.Set<RolePermission>().Remove(rolePermission);
        }
    }
}