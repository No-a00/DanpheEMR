using DanpheEMR.Core.Domain.BloodBank;
namespace DanpheEMR.Core.Interface.BloodBank
{
    public interface IBloodDonorRepository
    {
        Task<BloodDonor> GetByIdAsync(int id);
        Task<IEnumerable<BloodDonor>> GetAllAsync();
        Task<BloodDonor> AddAsync(BloodDonor donor);
        Task UpdateAsync(BloodDonor donor);

        // Không có hàm Delete 

        // Nghiệp vụ Ngân hàng máu
        Task<IEnumerable<BloodDonor>> SearchByNameOrContactAsync(string keyword);
        Task<IEnumerable<BloodDonor>> GetEligibleDonorsByBloodGroupAsync(int bloodGroupId);
        Task<IEnumerable<BloodDonor>> GetTopDonorsAsync(int minimumDonations);
        Task<IEnumerable<BloodDonor>> GetPermanentlyDeferredDonorsAsync();
    }
}