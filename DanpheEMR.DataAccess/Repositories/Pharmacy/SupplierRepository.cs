using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Pharmacy;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Pharmacy
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Supplier> _dbSet;

        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Supplier>();
        }

        public async Task<Supplier?> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<Supplier> AddAsync(Supplier supplier)
        {
            await _dbSet.AddAsync(supplier);
            return supplier;
        }

        public Task UpdateAsync(Supplier supplier)
        {
            _dbSet.Update(supplier);
            return Task.CompletedTask;
        }

        public async Task DeactivateSupplierAsync(int id, string cancelReason, int cancelledByUserId)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.CancelReason = cancelReason;
            result.CancelledByUserId = cancelledByUserId;
        }
        public async Task<IEnumerable<Supplier>> SearchSuppliersAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return new List<Supplier>();

            keyword = keyword.Trim();

            return await _dbSet.AsNoTracking()
                .Where(s => s.IsActive == true &&
                           (s.SupplierName.Contains(keyword) ||
                            s.SupplierCode.Contains(keyword) ||
                            s.ContactNumber.Contains(keyword))) 
                .OrderBy(s => s.SupplierName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Supplier>> GetActiveSuppliersAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(s => s.IsActive == true)
                .OrderBy(s => s.SupplierName)
                .ToListAsync();
        }
    }
}