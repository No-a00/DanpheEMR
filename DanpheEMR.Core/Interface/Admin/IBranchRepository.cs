using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Admin
{
    public interface IBranchRepository : IGenericRepository<Branch>
    {
        Task<bool> IsBranchNameExistsAsync(string branchName, Guid? excludeId = null);

    }
}