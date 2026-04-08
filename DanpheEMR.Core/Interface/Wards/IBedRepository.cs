
using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Wards
{
    public interface IBedRepository : IGenericRepository<Bed>
    {
        // Lễ tân tìm giường trống TRONG MỘT KHOA CỤ THỂ
        Task<IEnumerable<Bed>> GetAvailableBedsByWardAsync(Guid? wardId);

        // Bác sĩ cấp cứu tìm giường trống THEO LOẠI GIƯỜNG trên TOÀN BỆNH VIỆN
        Task<IEnumerable<Bed>> GetAvailableBedsByFeatureAsync(Guid bedFeatureId);
        // Điều dưỡng trưởng mở phần mềm lên xem Bản đồ giường của Khoa mình
        Task<IEnumerable<Bed>> GetBedsByWardIdAsync(Guid wardId);
        // Tổ trưởng Tổ Vệ sinh tìm danh sách các giường đang dơ để đi dọn dẹp
        Task<IEnumerable<Bed>> GetBedsByStatusAsync(BedStatus status);
    }
}