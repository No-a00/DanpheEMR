
using DanpheEMR.Core.Domain.Wards;

namespace DanpheEMR.Core.Interface.Wards
{
    public interface IWardsRepository
    {
        Task<Ward> GetByIdAsync(int id);
        Task<IEnumerable<Ward>> GetAllAsync(); 
        Task<Ward> AddAsync(Ward Ward);
        Task UpdateAsync(Ward Ward);
        Task<IEnumerable<Ward>> GetActiveWardssAsync();

        // CỰC KỲ HỮU ÍCH: Lấy thông tin Khoa KÈM THEO toàn bộ danh sách Giường bên trong 
        // (Dùng để Điều dưỡng trưởng vẽ "Bản đồ sơ đồ giường bệnh" trực quan trên màn hình)
        Task<Ward> GetWardsWithBedsAsync(int id);
    }
}