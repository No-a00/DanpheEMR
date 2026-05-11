
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.Core.Interface.Wards;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using DanpheEMR.DataAccess.Repositories.Base;

namespace DanpheEMR.DataAccess.Repositories.Wards
{
    public class BedRepository : GenericRepository<Bed>, IBedRepository
    {
        public BedRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Bed>> GetAvailableBedsByWardAsync(Guid? wardId)
        {
            return await _dbSet.AsNoTracking()
                .Where(b => b.WardId == wardId
                         && !b.IsDeleted
                         && b.IsOccupied == false 
                         && b.Status == BedStatus.Available) 
                .OrderBy(b => b.BedNumber) 
                .ToListAsync();
        }
        public async Task<IEnumerable<Bed>> GetAvailableBedsByFeatureAsync(Guid bedFeatureId)
        {
            return await _dbSet.AsNoTracking()
                .Where(b => b.BedFeatureId == bedFeatureId
                         && !b.IsDeleted 
                         && b.IsOccupied == false
                         && b.Status == BedStatus.Available)
                .Include(b => b.Ward)
                .OrderBy(b => b.Ward.WardName).ThenBy(b => b.BedNumber)
                .ToListAsync();
        }
        public async Task<IEnumerable<Bed>> GetBedsByWardIdAsync(Guid wardId)
        {
            return await _dbSet.AsNoTracking()
                .Where(b => b.WardId == wardId && !b.IsDeleted)
                .Include(b => b.BedFeature)
                .OrderBy(b => b.BedNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Bed>> GetBedsByStatusAsync(BedStatus status)
        {
            return await _dbSet.AsNoTracking()
                .Where(b => b.Status == status && !b.IsDeleted)
                .Include(b => b.Ward) 
                .OrderBy(b => b.Ward.WardName).ThenBy(b => b.BedNumber)
                .ToListAsync();
        }
        
        public async Task<string> GenerateBedCodeAsync()
        {
            string currentYear = DateTime.UtcNow.ToString("yy");
            string prefix = $"G{currentYear}";
            var lastPatient = await _dbSet
                .Where(p => p.BedCode != null && p.BedCode.StartsWith(prefix))
                .OrderByDescending(p => p.BedCode)
                .FirstOrDefaultAsync();
            if (lastPatient == null)
            {
                return $"{prefix}0001";
            }
            string lastSequenceStr = lastPatient.BedCode.Substring(prefix.Length);

            //  Cộng thêm 1 và format lại thành 4 chữ số
            if (int.TryParse(lastSequenceStr, out int lastSequence))
            {
                int nextSequence = lastSequence + 1;
                return $"{prefix}{nextSequence.ToString("D4")}";
            }
            return $"{prefix}0001";
        }
    }
}