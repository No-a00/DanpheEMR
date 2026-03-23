using DanpheEMR.Core.Domain.Wards;

namespace DanpheEMR.Core.Interface.Wards
{
    public interface IBedFeatureRepository
    {
        Task<BedFeature?> GetByIdAsync(Guid Id);

        Task<IEnumerable<BedFeature>> GetAllAsync();

        Task<BedFeature> AddAsync(BedFeature bedFeature);

        Task UpdateAsync(BedFeature bedFeature);

        Task DeactivateBedFeatureAsync(Guid Id, string cancelReason, int cancelledByUserId);
        Task<IEnumerable<BedFeature>> GetActiveBedFeaturesAsync();
    }
}