using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Wards
{
    public interface IBedFeatureRepository : IGenericRepository<BedFeature>
    {
        Task<IEnumerable<BedFeature>> GetActiveBedFeaturesAsync();
    }
}