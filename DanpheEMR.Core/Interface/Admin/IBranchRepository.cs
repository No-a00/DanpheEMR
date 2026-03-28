using DanpheEMR.Core.Domain.Admin;

namespace DanpheEMR.Core.Interface.Admin
{
    public interface IBranchRepository
    {
        Task<Branch> GetByIdAsync(Guid id);
        Task<IEnumerable<Branch>> GetAllAsync();
        Task<bool> IsBranchNameExistsAsync(string branchName, Guid? excludeId = null);
        Task AddAsync(Branch branch);
        void Update(Branch branch);
        void Delete(Branch branch);
    }
}