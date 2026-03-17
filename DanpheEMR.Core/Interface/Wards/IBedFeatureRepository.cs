using DanpheEMR.Core.Domain.Wards;

namespace DanpheEMR.Core.Interface.Wards
{
    public interface IBedFeatureRepository
    {
        Task<BedFeature> GetByIdAsync(int id);
        Task<IEnumerable<BedFeature>> GetAllAsync();
        Task<BedFeature> AddAsync(BedFeature bedFeature);
        Task UpdateAsync(BedFeature bedFeature);

        // Lấy danh sách các loại giường đang được bệnh viện cung cấp (Load lên Dropdown)
        Task<IEnumerable<BedFeature>> GetActiveBedFeaturesAsync();
    }
}