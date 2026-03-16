using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public interface ISupplierRepository
    {
        Task<Supplier> GetByIdAsync(int id);
        Task<IEnumerable<Supplier>> GetAllAsync();

        Task<Supplier> AddAsync(Supplier supplier);
        Task UpdateAsync(Supplier supplier);
        Task<IEnumerable<Supplier>> SearchSuppliersAsync(string keyword);
        Task<IEnumerable<Supplier>> GetActiveSuppliersAsync();
    }
}