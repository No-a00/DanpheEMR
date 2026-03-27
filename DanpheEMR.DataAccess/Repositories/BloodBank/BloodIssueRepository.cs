using DanpheEMR.Core.Domain.BloodBank;
using DanpheEMR.Core.Interface.BloodBank;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;


namespace DanpheEMR.DataAccess.Repositories.BloodBank
{
    public class BloodIssueRepository : IBloodIssueRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<BloodIssue> _dbSet;

        public BloodIssueRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<BloodIssue>();
        }

        public async Task AddAsync(BloodIssue issueRecord)
        {
            await _dbSet.AddAsync(issueRecord);
        }

        public async Task<BloodIssue?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(i => i.BloodInventory) 
                .Include(i => i.Patient)        
                .FirstOrDefaultAsync(i => i.Id == id);
        }

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