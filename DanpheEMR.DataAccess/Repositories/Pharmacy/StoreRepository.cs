using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Pharmacy;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Pharmacy
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Store> _dbSet;

        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Store>();
        }

        public async Task<Store?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Store>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<Store> AddAsync(Store store)
        {
            await _dbSet.AddAsync(store);
            return store;
        }

        public Task UpdateAsync(Store store)
        {
            _dbSet.Update(store);
            return Task.CompletedTask;
        }

        public async Task DeactivateStoreAsync(Guid id, string cancelReason, Guid cancelledByUserId)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.CancelReason = cancelReason;
            result.CancelledByUserId = cancelledByUserId;
        }
        public async Task<IEnumerable<Store>> GetActiveStoresAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(s => s.IsActive == true)
                .OrderBy(s => s.StoreName)
                .ToListAsync();
        }
    }
}