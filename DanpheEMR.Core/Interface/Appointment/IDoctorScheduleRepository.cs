using DanpheEMR.Core.Domain.Appointments;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interfaces.Appointment
{
    public interface IDoctorScheduleRepository : IGenericRepository<DoctorSchedule>
    {
        // 1. Lấy lịch của bác sĩ tại một thời điểm cụ thể (Dùng cho Handler)
        Task<DoctorSchedule?> GetDoctorScheduleAsync(Guid doctorId, DateTime appointmentDate);

        // 2. Lấy danh sách tất cả lịch của bác sĩ trong một ngày cụ thể
        Task<List<DoctorSchedule>> GetSchedulesByProviderIdAsync(Guid providerId, DateTime? startDate, DateTime? endDate);

        // 3. Tìm lịch theo Bác sĩ và Khoảng thời gian
        Task<DoctorSchedule?> GetScheduleByProviderIdAndTimeAsync(Guid providerId, DateTime date, TimeSpan time);

        // 4. Kiểm tra xem bác sĩ đã có lịch trong khoa vào khoảng thời gian đó chưa
        Task<bool> IsDoctorScheduledInDepartmentAsync(Guid providerId, Guid departmentId, DateTime date, TimeSpan startTime, TimeSpan endTime);
    }
}