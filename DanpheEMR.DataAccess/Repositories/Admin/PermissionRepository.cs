using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;


namespace DanpheEMR.DataAccess.Repositories.Admin
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Permission> _dbSet;

        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Permission>();
        }

        public async Task<Permission> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }


        public async Task AddRangeAsync(IEnumerable<Permission> permissions)
        {
            await _dbSet.AddRangeAsync(permissions);
        }

        public async Task<IEnumerable<Permission>> GetPermissionsByResourceAsync(string resource)
        {
            return await _dbSet.AsNoTracking().Where(p => p.Resource == resource).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllDistinctResourcesAsync()
        {
            return await _dbSet.AsNoTracking().Select(p => p.Resource).Distinct().ToListAsync();
        }

        // Đã sửa int thành Guid
        public async Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(Guid roleId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.RolePermissions.Any(rp => rp.RoleId == roleId))
                .ToListAsync();
        }

        // Đã sửa int thành Guid
        public async Task<bool> HasPermissionAsync(Guid roleId, string action, string resource)
        {
            return await _dbSet.AsNoTracking()
                .AnyAsync(p => p.Action == action && p.Resource == resource && p.RolePermissions.Any(rp => rp.RoleId == roleId));
        }

        public async Task<bool> RoleHasPermissionAsync(Guid roleId, Guid permissionId)
        {
            // Truy vấn vào bảng trung gian RolePermission
            return await _context.Set<RolePermission>()
                .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId && rp.IsActive);
        }

        public async Task AddRolePermissionAsync(RolePermission rolePermission)
        {
            await _context.Set<RolePermission>().AddAsync(rolePermission);
        }
    }
}