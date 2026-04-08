using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Pharmacy;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Pharmacy
{
    public class SupplierRepository : GenericRepository<Supplier>,ISupplierRepository
    {

        public SupplierRepository(ApplicationDbContext context) : base(context) { }
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