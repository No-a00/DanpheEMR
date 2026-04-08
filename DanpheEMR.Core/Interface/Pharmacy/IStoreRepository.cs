using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Pharmacy
{
    public interface IStoreRepository : IGenericRepository<Store>
    {
        Task<IEnumerable<Store>> GetActiveStoresAsync();
    }
}