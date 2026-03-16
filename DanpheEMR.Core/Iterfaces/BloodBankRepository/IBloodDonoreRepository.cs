using DanpheEMR.Core.Domain.Appointment;
using DanpheEMR.Core.Iterface.Base;

namespace DanpheEMR.Core.Iterfaces.BloodBankRepository
{
    public interface IBloodDonoreRepository : IGenericRepository<BloodDonor>
    {
        Task<IEnumerable<BloodDonor>> SearchByNameOrContactAsync(string keyword);
        Task<IEnumerable<BloodDonor>> GetEligibleDonorsByBloodGroupAsync(int bloodGroupId);  // tìm những người hiến máu đủ điều kiện theo nhóm máu (ví dụ: chỉ lấy những người hiến máu có nhóm máu A+)
        Task<IEnumerable<BloodDonor>> GetTopDonorsAsync(int minimumDonations); // tìm những người hiến máu có số lần hiến thành công lớn hơn hoặc bằng một ngưỡng nhất định (ví dụ: chỉ lấy những người hiến máu đã hiến thành công ít nhất 5 lần)
        Task<IEnumerable<BloodDonor>> GetPermanentlyDeferredDonorsAsync(); // tìm những người hiến máu bị cấm hiến vĩnh viễn (ví dụ: chỉ lấy những người hiến máu có cờ IsPermanentlyDeferred = true)
    }
}
