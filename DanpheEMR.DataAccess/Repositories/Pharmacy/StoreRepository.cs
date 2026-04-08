using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Pharmacy;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Pharmacy
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        public StoreRepository(ApplicationDbContext context) : base(context) { }
        public async Task<IEnumerable<Store>> GetActiveStoresAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(s => s.IsActive == true)
                .OrderBy(s => s.StoreName)
                .ToListAsync();
        }
    }
}