using DanpheEMR.Core.Domain.BloodBank;
using DanpheEMR.Core.Interface.BloodBank;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;


namespace DanpheEMR.DataAccess.Repositories.BloodBank
{
    public class BloodIssueRepository :GenericRepository<BloodIssue>, IBloodIssueRepository
    {
        
        public BloodIssueRepository(ApplicationDbContext context) : base(context) { }
        public async Task<IEnumerable<BloodIssue>> GetIssuesByPatientAsync(Guid patientId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(i => i.BloodInventory) 
                .ThenInclude(inv => inv.BloodGroup) 
                .Where(i => i.PatientId == patientId)
                .OrderByDescending(i => i.IssueDate) 
                .ToListAsync();
        }
    }
}