using DanpheEMR.Core.Domain.Pharmacy;
namespace DanpheEMR.Core.Interface.Pharmacy
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