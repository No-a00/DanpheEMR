using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Admin
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }


        public async Task<IEnumerable<User>> GetUsersWithRolesAsync()
        {
            return await _context.Set<User>()
                .Include(u => u.Employee)
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<IEnumerable<Permission>> GetPermissionsByUserIdAsync(Guid userId)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .SelectMany(u => u.UserRoles)
                .Select(ur => ur.Role)
                .SelectMany(r => r.RolePermissions)
                .Select(rp => rp.Permission)
                .Distinct()
                .ToListAsync();
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
                .AnyAsync(ur => ur.Id == userId && ur.RoleId == roleId && !ur.IsDeleted);
        }

        public async Task AddUserRoleAsync(UserRole userRole)
        {
            await _context.Set<UserRole>().AddAsync(userRole);
        }


        public async Task<UserRole?> GetUserRoleAsync(Guid userId, Guid roleId)
        {
            return await _context.Set<UserRole>()
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }

        public void RemoveUserRole(UserRole userRole)
        {
            _context.Set<UserRole>().Remove(userRole);
        }
        public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }
    }
}