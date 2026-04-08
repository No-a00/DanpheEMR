using DanpheEMR.Core.Domain.BloodBank;
using DanpheEMR.Core.Interface.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanpheEMR.Core.Interface.BloodBank
{
    public interface IBloodInventoryRepository : IGenericRepository<BloodInventory>
    {
        Task<List<BloodInventory>> GetAvailableBagsAsync(Guid bloodGroupId, int count);

        Task<IEnumerable<BloodInventory>> GetAllAvailableBagsAsync(Guid? BloodGroupId);
        Task<int> CountAvailableStockAsync(Guid bloodGroupId);
    }
}