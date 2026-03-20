

using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Admin
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<User> _dbSet;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<User>();
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        public async Task<User> AddAsync(User user)
        {
            var entry = await _dbSet.AddAsync(user);
            return entry.Entity;
        }
        public async Task UpdateAsync(User user)
        {
            _dbSet.Update(user);
        }
        public async Task DeactivateUserAsync(int id)
        {
            var user = await _dbSet.FindAsync(id);
            if (user != null)
            {
                user.IsActive = false;
                _dbSet.Update(user);
            }
        }
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
        }
        public async Task<User?> GetUserWithRolesAndPermissionsAsync(int userId)
        {
            return await _dbSet.AsNoTracking()
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<bool> CheckUserPermissionAsync(int userId, string action, string resource)
        {
            return await _dbSet.AsNoTracking()
                                .Where(u => u.Id == userId)
                                .SelectMany(u => u.UserRoles)
                                .Select(ur => ur.Role)
                                .SelectMany(r => r.RolePermissions)
                                .AnyAsync(rp => rp.Permission.Action == action && rp.Permission.Resource == resource);
        }
        public async Task<User?> GetUserWithEmployeeDetailsAsync(int userId)
        {
            return await _dbSet.AsNoTracking()
                .Include(u => u.Employee)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

    }
}
