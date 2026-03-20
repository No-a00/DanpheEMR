using DanpheEMR.Core.Domain.Billing;
using DanpheEMR.Core.Interfaces.Billing;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Billing
{
    public class ServiceItemRepository : GenericRepository<ServiceItem>, IServiceItemRepository
    {
        public ServiceItemRepository(ApplicationDbContext context) : base(context) { }
        public async Task<IEnumerable<ServiceItem>> SearchByNameOrCodeAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return new List<ServiceItem>();

            return await _dbSet.AsNoTracking()
                .Where(k => k.ItemCode.Contains(keyword) || k.ItemName.Contains(keyword))
                .ToListAsync();
        }
        public async Task<IEnumerable<ServiceItem>> GetItemsByCategoryAsync(int categoryId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.ServiceCategoryId == categoryId)
                .ToListAsync();
        }
        public async Task<bool> IsItemCodeExistsAsync(string itemCode, int? excludeId = null)
        {
            return await _dbSet.AsNoTracking()
                .AnyAsync(i => i.ItemCode == itemCode
                            && (!excludeId.HasValue || i.Id != excludeId.Value));
        }
    }
}