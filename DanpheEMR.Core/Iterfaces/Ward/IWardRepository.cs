using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanpheEMR.Core.Domain.Ward
{
    public interface IWardRepository
    {
        Task<Ward> GetByIdAsync(int id);
        Task<IEnumerable<Ward>> GetAllAsync(); 
        Task<Ward> AddAsync(Ward ward);
        Task UpdateAsync(Ward ward);
        Task<IEnumerable<Ward>> GetActiveWardsAsync();

        // CỰC KỲ HỮU ÍCH: Lấy thông tin Khoa KÈM THEO toàn bộ danh sách Giường bên trong 
        // (Dùng để Điều dưỡng trưởng vẽ "Bản đồ sơ đồ giường bệnh" trực quan trên màn hình)
        Task<Ward> GetWardWithBedsAsync(int id);
    }
}