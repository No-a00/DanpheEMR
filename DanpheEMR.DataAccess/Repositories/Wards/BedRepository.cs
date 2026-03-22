
using Application.Common.Enums;
using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.Core.Interface.Wards;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Wards
{
    public class BedRepository : IBedRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Bed> _dbSet;

        public BedRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Bed>();
        }

        public async Task<Bed?> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Bed> AddAsync(Bed bed)
        {
            await _dbSet.AddAsync(bed);
            return bed;
        }

        public Task UpdateAsync(Bed bed)
        {
            _dbSet.Update(bed);
            return Task.CompletedTask;
        }
        public async Task DeactivateBedAsync(int id, string cancelReason, int cancelledByUserId)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.CancelReason = cancelReason;
            result.CancelledByUserId = cancelledByUserId;
        }
        public async Task<IEnumerable<Bed>> GetAvailableBedsByWardAsync(int wardId)
        {
            return await _dbSet.AsNoTracking()
                .Where(b => b.WardId == wardId
                         && b.IsActive == true
                         && b.IsOccupied == false 
                         && b.Status == BedStatus.Available) 
                .OrderBy(b => b.BedNumber) 
                .ToListAsync();
        }
        public async Task<IEnumerable<Bed>> GetAvailableBedsByFeatureAsync(int bedFeatureId)
        {
            return await _dbSet.AsNoTracking()
                .Where(b => b.BedFeatureId == bedFeatureId
                         && b.IsActive == true
                         && b.IsOccupied == false
                         && b.Status == BedStatus.Available)
                .Include(b => b.Ward)
                .OrderBy(b => b.Ward.WardName).ThenBy(b => b.BedNumber)
                .ToListAsync();
        }
        public async Task<IEnumerable<Bed>> GetBedsByWardIdAsync(int wardId)
        {
            return await _dbSet.AsNoTracking()
                .Where(b => b.WardId == wardId && b.IsActive == true)
                .Include(b => b.BedFeature)
                .OrderBy(b => b.BedNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Bed>> GetBedsByStatusAsync(BedStatus status)
        {
            return await _dbSet.AsNoTracking()
                .Where(b => b.Status == status && b.IsActive == true)
                .Include(b => b.Ward) 
                .OrderBy(b => b.Ward.WardName).ThenBy(b => b.BedNumber)
                .ToListAsync();
        }
    }
}