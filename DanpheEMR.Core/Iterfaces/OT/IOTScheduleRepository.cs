using DanpheEMR.Core.Domain.Appointment;


namespace DanpheEMR.Core.Domain.OT
{
    public interface IOTScheduleRepository
    {
      
        Task<OTSchedule> GetByIdAsync(int id);
        Task<OTSchedule> AddAsync(OTSchedule schedule);
        Task UpdateAsync(OTSchedule schedule);
        Task CancelScheduleAsync(int id, string cancelReason, int cancelledByUserId);
        Task<IEnumerable<OTSchedule>> GetSchedulesByDateAsync(DateTime date);

        // Lọc danh sách theo Loại phẫu thuật (VD: Tìm các ca "Mổ ruột thừa")
        Task<IEnumerable<OTSchedule>> GetSchedulesByTypeAsync(string surgeryType);
        // Trưởng khoa Ngoại muốn xem hôm nay Bác sĩ A phải mổ mấy ca
        Task<IEnumerable<OTSchedule>> GetSchedulesBySurgeonAsync(int surgeonId, DateTime date);

        // Điều dưỡng phòng mổ muốn xem lịch của Phòng mổ số 1 hôm nay
        Task<IEnumerable<OTSchedule>> GetSchedulesByRoomAsync(int roomId, DateTime date);
        // Trước khi Add lịch mới, phải gọi hàm này để kiểm tra xem Phòng mổ đó có đang trống trong khung giờ đó không.
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime date, TimeSpan startTime, TimeSpan endTime);
    }
}