using DanpheEMR.Core.Domain.BloodBank;
using DanpheEMR.Core.Interface.Base;
namespace DanpheEMR.Core.Interface.BloodBank
{
    public interface IBloodDonoreRepository : IGenericRepository<BloodDonor>
    {
        // Nghiệp vụ Ngân hàng máu
        Task<IEnumerable<BloodDonor>> SearchByNameOrContactAsync(string keyword);
        Task<IEnumerable<BloodDonor>> GetEligibleDonorsByBloodGroupAsync(int bloodGroupId);
        Task<IEnumerable<BloodDonor>> GetTopDonorsAsync(int minimumDonations);
        Task<IEnumerable<BloodDonor>> GetPermanentlyDeferredDonorsAsync();
    }
}