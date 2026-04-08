using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.Core.Interface.Wards;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Wards
{
    public class BedFeatureRepository : GenericRepository<BedFeature>,IBedFeatureRepository
    {

        public BedFeatureRepository(ApplicationDbContext context) : base(context) { }


        public async Task<IEnumerable<BedFeature>> GetActiveBedFeaturesAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(b => !b.IsDeleted )
                .OrderBy(b => b.FeatureName)
                .ToListAsync();
        }
    }
}