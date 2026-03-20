using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.Core.Interface.Wards;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Wards
{
    public class BedFeatureRepository : IBedFeatureRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<BedFeature> _dbSet;

        public BedFeatureRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<BedFeature>();
        }

        public async Task<BedFeature?> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<BedFeature>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<BedFeature> AddAsync(BedFeature bedFeature)
        {
            await _dbSet.AddAsync(bedFeature);
            return bedFeature;
        }

        public Task UpdateAsync(BedFeature bedFeature)
        {
            _dbSet.Update(bedFeature);
            return Task.CompletedTask;
        }
        public async Task DeactivateBedFeatureAsync(int id, string cancelReason, int cancelledByUserId)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.CancelReason = cancelReason;
            result.CancelledByUserId = cancelledByUserId;
        }

        public async Task<IEnumerable<BedFeature>> GetActiveBedFeaturesAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(b => b.IsActive == true)
                .OrderBy(b => b.FeatureName)
                .ToListAsync();
        }
    }
}