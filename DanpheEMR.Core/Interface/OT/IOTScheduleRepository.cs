using DanpheEMR.Core.Domain.OT;
using DanpheEMR.Core.Interface.Base; // Nhớ using IGenericRepository

namespace DanpheEMR.Core.Interface.OT {

    public interface IOTScheduleRepository : IGenericRepository<OTSchedule>
    {
 

        // Lấy tất cả ca mổ trong một ngày cụ thể
        Task<IEnumerable<OTSchedule>> GetSchedulesByDateAsync(DateTime date);

        // Lọc danh sách theo Loại phẫu thuật (VD: "Mổ ruột thừa")
        Task<IEnumerable<OTSchedule>> GetSchedulesByTypeAsync(string surgeryType);

        //  Trưởng khoa Ngoại muốn xem hôm nay Bác sĩ A phải mổ mấy ca
        Task<IEnumerable<OTSchedule>> GetSchedulesBySurgeonAsync(Guid surgeonId, DateTime date);

        //  Điều dưỡng phòng mổ muốn xem lịch của Phòng mổ số 1 hôm nay
        Task<IEnumerable<OTSchedule>> GetSchedulesByRoomAsync(Guid roomId, DateTime date);

        //Kiểm tra xem Phòng mổ có đang trống trong khung giờ đó không
        Task<bool> IsRoomAvailableAsync(Guid roomId, DateTime date, TimeSpan startTime, TimeSpan endTime);

        // Kiểm tra xem Bác sĩ phẫu thuật có bị kíp mổ khác trùng giờ không
        Task<bool> IsSurgeonAvailableAsync(Guid surgeonId, DateTime date, TimeSpan startTime, TimeSpan endTime);
    }
}