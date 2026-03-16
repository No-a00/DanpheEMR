using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanpheEMR.Core.Domain.Pharmacy
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