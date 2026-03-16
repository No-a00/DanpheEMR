using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanpheEMR.Core.Domain.Ward
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