using DanpheEMR.Core.Domain.OT;
using DanpheEMR.Core.Interface.Base; // Nhớ using IGenericRepository

namespace DanpheEMR.Core.Interface.OT

    public interface IOTScheduleRepository : IGenericRepository<OTSchedule>
    {
        // --- NHÓM LẤY DANH SÁCH (QUERIES) ---

        // 1. Lấy tất cả ca mổ trong một ngày cụ thể
        Task<IEnumerable<OTSchedule>> GetSchedulesByDateAsync(DateTime date);

        // 2. Lọc danh sách theo Loại phẫu thuật (VD: "Mổ ruột thừa")
        Task<IEnumerable<OTSchedule>> GetSchedulesByTypeAsync(string surgeryType);

        // 3. Trưởng khoa Ngoại muốn xem hôm nay Bác sĩ A phải mổ mấy ca
        Task<IEnumerable<OTSchedule>> GetSchedulesBySurgeonAsync(Guid surgeonId, DateTime date);

        // 4. Điều dưỡng phòng mổ muốn xem lịch của Phòng mổ số 1 hôm nay
        Task<IEnumerable<OTSchedule>> GetSchedulesByRoomAsync(Guid roomId, DateTime date);

        // --- NHÓM KIỂM TRA RÀNG BUỘC (VALIDATIONS) ---

        // 5. Kiểm tra xem Phòng mổ có đang trống trong khung giờ đó không
        Task<bool> IsRoomAvailableAsync(Guid roomId, DateTime date, TimeSpan startTime, TimeSpan endTime);

        // TẶNG THÊM: 6. Kiểm tra xem Bác sĩ phẫu thuật có bị kíp mổ khác trùng giờ không
        Task<bool> IsSurgeonAvailableAsync(Guid surgeonId, DateTime date, TimeSpan startTime, TimeSpan endTime);
    }
}