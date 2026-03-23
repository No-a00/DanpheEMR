
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
        public async Task<IEnumerable<Permission>> GetPermissionsByResourceAsync(string resource)
        {
            return await _dbSet.AsNoTracking().Where(p => p.Resource == resource).ToListAsync();
        }
        public async Task<IEnumerable<string>> GetAllDistinctResourcesAsync()
        {
            return await _dbSet.AsNoTracking().Select(p => p.Resource).Distinct().ToListAsync();
        }
        public async Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(int roleId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.RolePermissions.Any(rp => rp.RoleId == roleId))
                .ToListAsync();
        }
        public async Task<bool> HasPermissionAsync(int roleId, string action, string resource)
        {
            return await _dbSet.AsNoTracking()
                .AnyAsync(p => p.Action == action && p.Resource == resource && p.RolePermissions.Any(rp => rp.RoleId == roleId));
        }
    }
}
