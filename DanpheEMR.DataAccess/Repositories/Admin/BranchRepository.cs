using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.DataAccess.Data; 
using Microsoft.EntityFrameworkCore;


namespace DanpheEMR.DataAccess.Repositories.Admin
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext _context;

        public BranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Branch> GetByIdAsync(Guid id)
        {
            return await _context.Set<Branch>().FindAsync(id);
        }

        public async Task<IEnumerable<Branch>> GetAllAsync()
        {
            return await _context.Set<Branch>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> IsBranchNameExistsAsync(string branchName, Guid? excludeId = null)
        {
            var query = _context.Set<Branch>().AsQueryable();
            if (excludeId.HasValue)
            {
                query = query.Where(b => b.Id != excludeId.Value);
            }

            return await query.AnyAsync(b => b.BranchName.ToLower() == branchName.ToLower());
        }

        public async Task AddAsync(Branch branch)
        {
            await _context.Set<Branch>().AddAsync(branch);
        }

        public void Update(Branch branch)
        {
            _context.Set<Branch>().Update(branch);
        }

        public void Delete(Branch branch)
        {
            _context.Set<Branch>().Remove(branch);
        }
    }
}