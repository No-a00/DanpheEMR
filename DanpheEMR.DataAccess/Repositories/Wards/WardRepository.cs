using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.Core.Interface.Wards;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Wards
{
    public class WardRepository : IWardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Ward> _dbSet;

        public WardRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Ward>();
        }

        public async Task<Ward?> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<IEnumerable<Ward>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<Ward> AddAsync(Ward ward)
        {
            await _dbSet.AddAsync(ward);
            return ward;
        }

        public Task UpdateAsync(Ward ward)
        {
            _dbSet.Update(ward);
            return Task.CompletedTask;
        }
        public async Task DeactivateWardAsync(int id, string cancelReason, int cancelledByUserId)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.CancelReason = cancelReason;
            result.CancelledByUserId = cancelledByUserId;
        }
        public async Task<IEnumerable<Ward>> GetActiveWardsAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(w => w.IsActive == true)
                .OrderBy(w => w.WardName) 
                .ToListAsync();
        }
        public async Task<Ward?> GetWardWithBedsAsync(int id)
        {
            return await _dbSet.AsNoTracking()
                .Include(w => w.Beds) 
                .FirstOrDefaultAsync(w => w.Id == id && w.IsActive == true);
        }
    }
}