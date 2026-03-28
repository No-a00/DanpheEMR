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

        public async Task<User?> GetByIdAsync(Guid id)
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
            await Task.CompletedTask;
        }

        public async Task DeactivateUserAsync(Guid id)
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
            // Lưu ý: Nếu Entity của bạn là 'Username' (chữ n thường) thì nhớ sửa lại nhé
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User?> GetUserWithRolesAndPermissionsAsync(Guid userId)
        {
            return await _dbSet.AsNoTracking()
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> CheckUserPermissionAsync(Guid userId, string action, string resource)
        {
            return await _dbSet.AsNoTracking()
                .Where(u => u.Id == userId)
                .SelectMany(u => u.UserRoles)
                .Select(ur => ur.Role)
                .SelectMany(r => r.RolePermissions)
                .AnyAsync(rp => rp.Permission.Action == action && rp.Permission.Resource == resource);
        }

        public async Task<User?> GetUserWithEmployeeDetailsAsync(Guid userId)
        {
            return await _dbSet.AsNoTracking()
                .Include(u => u.Employee)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            return !await _dbSet.AnyAsync(u => u.UserName.ToLower() == username.ToLower());
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await _dbSet.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }
        public async Task<bool> UserHasRoleAsync(Guid userId, Guid roleId)
        {
            return await _context.Set<UserRole>()
                .AnyAsync(ur => ur.Id == userId && ur.RoleId == roleId && ur.IsActive);
        }

        public async Task AddUserRoleAsync(UserRole userRole)
        {
            await _context.Set<UserRole>().AddAsync(userRole);
        }
    }
}