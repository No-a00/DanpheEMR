
using DanpheEMR.Core.Domain.BloodBank;
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Interface.BloodBank;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.BloodBank
{
    public  class BloodGroupRepository :GenericRepository<BloodGroup>, IBloodGroupRepository
    {
 
        public BloodGroupRepository(ApplicationDbContext context) : base(context) { }
       
        
        public async Task<BloodGroup?> GetByNameAsync(BloodType bloodGroupName)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(b => b.BloodGroupName == bloodGroupName);
        }       
        public async Task<BloodGroup?> GetWithDonorsAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                               .Include(x => x.BloodDonors)
                               .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
