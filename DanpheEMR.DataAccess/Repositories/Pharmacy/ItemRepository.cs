using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Pharmacy;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Pharmacy
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Item> _dbSet;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Item>();
        }

        public async Task<Item?> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Item> AddAsync(Item item)
        {
            await _dbSet.AddAsync(item);
            return item;
        }

        public Task UpdateAsync(Item item)
        {
            _dbSet.Update(item);
            return Task.CompletedTask;
        }

        public async Task DeactivateItemAsync(int id, string cancelReason, int cancelUserId)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.CancelReason = cancelReason;
            result.CancelUserId = cancelUserId;
        }

        public async Task<IEnumerable<Item>> GetItemsBySubCategoryAsync(int subCategoryId)
        {
            return await _dbSet.AsNoTracking()
                .Where(x => x.SubCategoryId == subCategoryId && x.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Item>> SearchItemsAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return new List<Item>();

            keyword = keyword.Trim();

            return await _dbSet.AsNoTracking()
                .Where(x => x.IsActive &&
                           (x.ItemName.Contains(keyword) || x.ItemCode.Contains(keyword)))
                .OrderBy(x => x.ItemName)
                .ToListAsync();
        }
        public async Task<IEnumerable<Item>> GetItemsNearingReorderLevelAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(x => x.IsActive && x.ReorderLevel <= x.ReorderLevel)
                .OrderBy(x => x.ReorderLevel) // Thằng nào tồn kho ít nhất thì nổi lên đầu
                .ToListAsync();
        }
    }
}