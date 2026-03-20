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

        public async Task<IEnumerable<BloodDonor>> GetEligibleDonorsByBloodGroupAsync(int bloodGroupId)
        {
            return await _dbSet.AsNoTracking() 
                .Where(b => b.BloodGroupId == bloodGroupId && b.IsEligibleToDonate)
                .ToListAsync();
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