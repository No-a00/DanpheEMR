
using DanpheEMR.Core.Domain.Appointments;
using DanpheEMR.Core.Interfaces.Appointment;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Appointments
{
    public class ServiceCategoryRepository : GenericRepository<ServiceCategory>, IServiceCategoryRepository
    {
        public ServiceCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<ServiceCategory?> GetByCategoryCodeAsync(string categoryCode)
        {
            ServiceCategory? category = await _dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryCode == categoryCode);
            if (category == null)
            {
                return null;
            }
            return category;
        }
        public async Task<bool> IsCodeExistsAsync(string categoryCode)
        {
            return await _dbSet.AnyAsync(c => c.CategoryCode == categoryCode);
        }
        public async Task<IEnumerable<ServiceCategory>> GetAllWithItemsAsync()
        {
            return await _dbSet.Include(c => c.ServiceItems).AsNoTracking().ToListAsync();
        }

    }
}
