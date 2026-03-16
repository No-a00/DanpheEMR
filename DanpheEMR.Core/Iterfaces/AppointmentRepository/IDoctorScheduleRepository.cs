using DanpheEMR.Core.Domain.Appointment;
using DanpheEMR.Core.Iterface.Base;
using System.Numerics;

namespace DanpheEMR.Core.Iterfaces.Appointment
{
    public interface  IDoctorScheduleRepository : IGenericRepository<DoctorSchedule>
    {
        //Lấy tất cả các khung giờ làm việc đã cài đặt của một Bác sĩ
        public Task<List<DoctorSchedule>> GetShedulesByProviderIdAsync(int providerId, int dayOfWeek);
        //Lấy tất cả các khung giờ làm việc đã cài đặt của một khoa trong ngày
        public Task<List<DoctorSchedule>> GetSchedulesByDepartmentAndDayAsync(int departmentId, int dayOfWeek);
        //Tìm cấu hình lịch chứa khoảng thời gian người dùng muốn đặt
        public Task<DoctorSchedule> GetShedulesByProviderIdAndTimeAsync(int providerId, int dayOfWeek, TimeSpan time);
        // kiểm tra xem bác sĩ đã có lịch làm việc trong ngày chưa trong khoa trong ngày chưa
        public Task<bool> IsDoctorScheduledInDepartmentAsync(int providerId, int departmentId, int dayOfWeek,TimeSpan StartTime, TimeSpan EndTime);
    }
}
