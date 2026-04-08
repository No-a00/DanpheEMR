using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Wards
{
    public interface IWardRepository : IGenericRepository<Ward>
    {
        Task<IEnumerable<Ward>> GetActiveWardsAsync();
        // Dùng để Điều dưỡng trưởng vẽ "Bản đồ sơ đồ giường bệnh" trực quan trên màn hình
        Task<Ward?> GetWardWithBedsAsync(Guid Id);
    }
}