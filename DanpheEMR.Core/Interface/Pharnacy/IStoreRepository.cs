using DanpheEMR.Core.Domain.Pharmacy;
namespace DanpheEMR.Core.Interface.Pharmacy
{
    public interface IStoreRepository
    {
        Task<Store> GetByIdAsync(int id);
        Task<IEnumerable<Store>> GetAllAsync();

        Task<Store> AddAsync(Store store);
        Task UpdateAsync(Store store);

        Task<IEnumerable<Store>> GetActiveStoresAsync();
    }
}