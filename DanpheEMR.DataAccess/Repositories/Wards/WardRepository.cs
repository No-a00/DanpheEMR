using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.Core.Interface.Wards;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Wards
{
    public class WardRepository : GenericRepository<Ward>,IWardRepository
    {

        public WardRepository(ApplicationDbContext context): base(context) { }
   
        public async Task<IEnumerable<Ward>> GetActiveWardsAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(w => !w.IsDeleted)
                .OrderBy(w => w.WardName) 
                .ToListAsync();
        }
        public async Task<Ward?> GetWardWithBedsAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Include(w => w.Beds) 
                .FirstOrDefaultAsync(w => w.Id == id && !w.IsDeleted );
        }
    }
}