using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Pharmacy;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Pharmacy
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {

        public ItemRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Item>> GetItemsBySubCategoryAsync(Guid subCategoryId)
        {
            return await _dbSet.AsNoTracking()
                .Where(x => x.SubCategoryId == subCategoryId && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Item>> SearchItemsAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return new List<Item>();

            keyword = keyword.Trim();

            return await _dbSet.AsNoTracking()
                .Where(x => !x.IsDeleted&&
                           (x.ItemName.Contains(keyword) || x.ItemCode.Contains(keyword)))
                .OrderBy(x => x.ItemName)
                .ToListAsync();
        }
        public async Task<IEnumerable<Item>> GetItemsNearingReorderLevelAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(x => !x.IsDeleted && x.ReorderLevel <= x.ReorderLevel)
                .OrderBy(x => x.ReorderLevel)
                .ToListAsync();
        }
    }
}