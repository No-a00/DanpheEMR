using DanpheEMR.Core.Domain.Wards;

namespace DanpheEMR.Core.Interface.Wards
{
    public interface IWardRepository
    {
        Task<Ward?> GetByIdAsync(int id);

        Task<IEnumerable<Ward>> GetAllAsync();

        Task<Ward> AddAsync(Ward ward);

        Task UpdateAsync(Ward ward);
        Task DeactivateWardAsync(int id, string cancelReason, int cancelledByUserId);

        Task<IEnumerable<Ward>> GetActiveWardsAsync();

        // CỰC KỲ HỮU ÍCH: Lấy thông tin 1 Khoa (số ít) KÈM THEO toàn bộ danh sách Giường 
        // (Dùng để Điều dưỡng trưởng vẽ "Bản đồ sơ đồ giường bệnh" trực quan trên màn hình)
        Task<Ward?> GetWardWithBedsAsync(int id);
    }
}