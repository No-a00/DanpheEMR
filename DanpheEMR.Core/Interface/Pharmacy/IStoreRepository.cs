using DanpheEMR.Core.Domain.Pharmacy;

namespace DanpheEMR.Core.Interface.Pharmacy
{
    public interface IStoreRepository
    {
        Task<Store?> GetByIdAsync(Guid Id);

        Task<IEnumerable<Store>> GetAllAsync();

        Task<Store> AddAsync(Store store);

        Task UpdateAsync(Store store);

        Task DeactivateStoreAsync(Guid Id, string cancelReason, int cancelledByUserId);
        Task<IEnumerable<Store>> GetActiveStoresAsync();
    }
}