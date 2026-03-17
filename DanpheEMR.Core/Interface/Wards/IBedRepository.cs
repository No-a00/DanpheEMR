using DanpheEMR.Core.Domain.Wards;
namespace DanpheEMR.Core.Interface.Wards
{
    public interface IBedRepository
    {
        Task<Bed> GetByIdAsync(int id);
        Task<Bed> AddAsync(Bed bed);
        Task UpdateAsync(Bed bed);

        // Lễ tân tìm giường trống TRONG MỘT KHOA CỤ THỂ để làm thủ tục Nhập viện
        // (Điều kiện: IsOccupied = false VÀ Status = "Available")
        Task<IEnumerable<Bed>> GetAvailableBedsByWardsAsync(int WardsId);

        // Bác sĩ cấp cứu tìm giường trống THEO LOẠI GIƯỜNG trên TOÀN BỆNH VIỆN
        // (VD: "Tìm gấp cho tôi 1 giường Hồi sức tích cực - ICU FeatureId đang trống!")
        Task<IEnumerable<Bed>> GetAvailableBedsByFeatureAsync(int bedFeatureId);

        // Điều dưỡng trưởng mở phần mềm lên xem toàn bộ Bản đồ giường của Khoa mình
        Task<IEnumerable<Bed>> GetBedsByWardsIdAsync(int WardsId);

        // Tổ trưởng Tổ Vệ sinh tìm danh sách các giường đang dơ để đi dọn dẹp
        // (Điều kiện lọc: Status = "Cleaning")
        Task<IEnumerable<Bed>> GetBedsByStatusAsync(string status);
    }
}