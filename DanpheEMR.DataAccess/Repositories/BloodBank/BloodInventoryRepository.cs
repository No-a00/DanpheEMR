using DanpheEMR.Core.Domain.BloodBank;
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Interface.BloodBank;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;


namespace DanpheEMR.DataAccess.Repositories.BloodBank
{
    public class BloodInventoryRepository : GenericRepository<BloodInventory>, IBloodInventoryRepository
    {

        public BloodInventoryRepository(ApplicationDbContext context) : base(context) { }



        public async Task<List<BloodInventory>> GetAvailableBagsAsync(Guid bloodGroupId, int count)
        {
            return await _dbSet

                .Where(b => b.BloodGroupId == bloodGroupId

                         && b.Status == BloodBagStatus.Available

                         && b.ExpiryDate >= DateTime.Now)

                .OrderBy(b => b.ExpiryDate)

                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<BloodInventory>> GetAllAvailableBagsAsync(Guid? bloodGroupId)
        {

            var query = _dbSet
                .Include(b => b.BloodGroup)
                .Where(b => b.Status == BloodBagStatus.Available && b.ExpiryDate >= DateTime.Now);
            if (bloodGroupId.HasValue)
            {
                query = query.Where(b => b.BloodGroupId == bloodGroupId.Value);
            }

            return await query.OrderBy(b => b.ExpiryDate).ToListAsync();
        }
        public async Task<int> CountAvailableStockAsync(Guid bloodGroupId)
        {
            return await _dbSet.CountAsync(b =>
                b.BloodGroupId == bloodGroupId
                && b.Status == BloodBagStatus.Available
                && b.ExpiryDate >= DateTime.Now);
        }
    }
}