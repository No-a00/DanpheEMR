using DanpheEMR.Core.Domain.Wards;

namespace DanpheEMR.Core.Interface.Wards
{
    public interface IBedFeatureRepository
    {
        Task<BedFeature?> GetByIdAsync(int id);

        Task<IEnumerable<BedFeature>> GetAllAsync();

        Task<BedFeature> AddAsync(BedFeature bedFeature);

        Task UpdateAsync(BedFeature bedFeature);

        Task DeactivateBedFeatureAsync(int id, string cancelReason, int cancelledByUserId);
        Task<IEnumerable<BedFeature>> GetActiveBedFeaturesAsync();
    }
}