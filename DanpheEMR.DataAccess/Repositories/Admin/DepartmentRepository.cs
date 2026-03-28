using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanpheEMR.DataAccess.Repositories.Admin
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Department>> GetClinicalDepartmentsAsync()
        {
            return await _context.Set<Department>()
                .Where(d => d.IsClinical == true && d.IsActive == true)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetRootDepartmentsAsync()
        {
            return await _context.Set<Department>()
                .Where(d => d.ParentDepartmentId == null && d.IsActive == true)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetSubDepartmentsAsync(Guid parentDepartmentId)
        {
            return await _context.Set<Department>()
                .Where(d => d.ParentDepartmentId == parentDepartmentId && d.IsActive == true)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Department?> GetDepartmentWithEmployeesAsync(Guid departmentId)
        {
            return await _context.Set<Department>()
                .Include(d => d.Employees) 
                .FirstOrDefaultAsync(d => d.Id == departmentId);
        }

        public async Task<bool> IsCodeExistsAsync(string departmentCode, Guid? excludeId = null)
        {
            var query = _context.Set<Department>().AsQueryable();

            if (excludeId.HasValue)
            {
                query = query.Where(d => d.Id != excludeId.Value);
            }
            return await query.AnyAsync(d => d.DepartmentCode.ToLower() == departmentCode.ToLower());
        }
    }
}