using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;


namespace DanpheEMR.DataAccess.Repositories.Admin
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        public BranchRepository(ApplicationDbContext context) : base(context) { }

        public async Task<bool> IsBranchNameExistsAsync(string branchName, Guid? excludeId = null)
        {
            var query = _dbSet.AsQueryable();
            if (excludeId.HasValue)
            {
                query = query.Where(b => b.Id != excludeId.Value);
            }

            return await query.AnyAsync(b => b.BranchName.ToLower() == branchName.ToLower());
        }

       
    }
}