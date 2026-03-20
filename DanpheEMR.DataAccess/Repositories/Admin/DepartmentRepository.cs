using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base; 
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Admin
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Department>> GetClinicalDepartmentsAsync()
        {
            return await _dbSet
                .AsNoTracking() 
                .Where(d => d.IsClinical == true && d.IsActive == true) 
                .ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetRootDepartmentsAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(d => d.ParentDepartmentId == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetSubDepartmentsAsync(int parentDepartmentId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(d => d.ParentDepartmentId == parentDepartmentId)
                .ToListAsync();
        }

        public async Task<Department> GetDepartmentWithEmployeesAsync(int departmentId)
        {
            return await _dbSet
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.Id == departmentId);
        }

        public async Task<bool> IsCodeExistsAsync(string departmentCode, int? excludeId = null)
        {
            if (excludeId.HasValue)
            {
                // Dùng cho trường hợp CẬP NHẬT (Bỏ qua Id của chính nó)
                return await _dbSet.AnyAsync(d => d.DepartmentCode == departmentCode && d.Id != excludeId.Value);
            }

            // Dùng cho trường hợp THÊM MỚI
            return await _dbSet.AnyAsync(d => d.DepartmentCode == departmentCode);
        }
    }
}