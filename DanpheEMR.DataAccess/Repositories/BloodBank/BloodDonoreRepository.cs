using DanpheEMR.Core.Domain.BloodBank;
using DanpheEMR.Core.Interface.BloodBank;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
// Bỏ using System.Security.Cryptography.X509Certificates; đi vì không cần thiết

namespace DanpheEMR.DataAccess.Repositories.BloodBank
{
    public class BloodDonoreRepository : GenericRepository<BloodDonor>,IBloodDonoreRepository
    {
        public BloodDonoreRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BloodDonor>> SearchByNameOrContactAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return new List<BloodDonor>();
            return await _dbSet.AsNoTracking()
                .Where(x => x.DonorName.Contains(keyword) || x.Contact.Contains(keyword))
                .ToListAsync();
        }

        public async Task<IEnumerable<BloodDonor>> GetEligibleDonorsAsync(Guid? bloodGroupId)
        {
            DateTime eighteenYearsAgo = DateTime.Today.AddYears(-18);
            DateTime sixtyYearsAgo = DateTime.Today.AddYears(-60);
            DateTime eightyFourDaysAgo = DateTime.Today.AddDays(-84);

            var query = _dbSet
                .Include(d => d.BloodGroup)
                .Where(d => !d.IsDeleted
                         && !d.IsPermanentlyDeferred
                         && d.Weight >= 45
                        
                         && d.DateOfBirth <= eighteenYearsAgo
                         && d.DateOfBirth >= sixtyYearsAgo
                         
                         && (d.LastDonatedDate == null || d.LastDonatedDate <= eightyFourDaysAgo));

            if (bloodGroupId.HasValue)
            {
                query = query.Where(d => d.BloodGroupId == bloodGroupId.Value);
            }

            return await query.OrderBy(d => d.LastDonatedDate).ToListAsync();
        }

        public async Task<IEnumerable<BloodDonor>> GetTopDonorsAsync(int minimumDonations)
        {
            return await _dbSet.AsNoTracking()
                .Where(d => d.TotalDonations >= minimumDonations)
                .OrderByDescending(d => d.TotalDonations)
                .ToListAsync();
        }

        public async Task<IEnumerable<BloodDonor>> GetPermanentlyDeferredDonorsAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(d => d.IsPermanentlyDeferred == true)
                .ToListAsync();
        }
    }
}