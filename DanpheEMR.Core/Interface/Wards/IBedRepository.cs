using Application.Common.Enums;
using DanpheEMR.Core.Domain.Wards;

namespace DanpheEMR.Core.Interface.Wards
{
    public interface IBedRepository
    {
        Task<Bed?> GetByIdAsync(int id);
        Task<Bed> AddAsync(Bed bed);
        Task UpdateAsync(Bed bed);

   
        Task DeactivateBedAsync(int id, string cancelReason, int cancelledByUserId);
        // Lễ tân tìm giường trống TRONG MỘT KHOA CỤ THỂ
        Task<IEnumerable<Bed>> GetAvailableBedsByWardAsync(int wardId);

        // Bác sĩ cấp cứu tìm giường trống THEO LOẠI GIƯỜNG trên TOÀN BỆNH VIỆN
        Task<IEnumerable<Bed>> GetAvailableBedsByFeatureAsync(int bedFeatureId);
        // Điều dưỡng trưởng mở phần mềm lên xem Bản đồ giường của Khoa mình
        Task<IEnumerable<Bed>> GetBedsByWardIdAsync(int wardId);
        // Tổ trưởng Tổ Vệ sinh tìm danh sách các giường đang dơ để đi dọn dẹp
        Task<IEnumerable<Bed>> GetBedsByStatusAsync(BedStatus status);
    }
}