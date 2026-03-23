
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Admin
{
    public  class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Employee>> SearchByNameOrContactAsync(string keyword)
        {
            var query = _dbSet.Where(e => e.FirstName.Contains(keyword) || e.LastName.Contains(keyword) || e.ContactNumber.Contains(keyword));
            return await Task.FromResult(query.AsEnumerable());
        }
        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int departmentId) { 
            return await _dbSet
                .AsNoTracking()
                .Where(e => e.DepartmentId == departmentId)
                .ToListAsync(); 
        }

        public async Task<Employee> GetEmployeeByUserIdAsync(int userId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(e => e.Users)
                .FirstOrDefaultAsync(e => e.Users.Any(u => u.Id == userId));
        }
        public async Task<Employee> GetEmployeeWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(e => e.Department)
                .Include(e => e.Users)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

    }
}
