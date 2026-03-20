

using DanpheEMR.Core.Domain.Appointments;
using DanpheEMR.Core.Interfaces.Appointment;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Appointments
{
    public class DoctorScheduleRepository : GenericRepository<DoctorSchedule>, IDoctorScheduleRepository
    {
        public DoctorScheduleRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async  Task<List<DoctorSchedule>> GetShedulesByProviderIdAsync(int providerId, int dayOfWeek)
        {
            return await _dbSet
                .Where(ds => ds.ProviderId == providerId && ds.DayOfWeek == dayOfWeek)
                .ToListAsync();
        }
        //Lấy tất cả các khung giờ làm việc đã cài đặt của một khoa trong ngày
        public async Task<List<DoctorSchedule>> GetSchedulesByDepartmentAndDayAsync(int departmentId, int dayOfWeek)
        {
            return await _dbSet
                .Where(ds => ds.DepartmentId == departmentId && ds.DayOfWeek == dayOfWeek)
                .ToListAsync();
        }
        //Tìm cấu hình lịch chứa khoảng thời gian người dùng muốn đặt
        public async Task<DoctorSchedule?> GetShedulesByProviderIdAndTimeAsync(int providerId, int dayOfWeek, TimeSpan time)
        {
            return await _dbSet
                .Where(ds => ds.ProviderId == providerId && ds.DayOfWeek == dayOfWeek
                    && ds.StartTime <= time && ds.EndTime > time)
                .FirstOrDefaultAsync();
        }
        // kiểm tra xem bác sĩ đã có lịch làm việc trong ngày chưa trong khoa trong ngày chưa
        public async Task<bool> IsDoctorScheduledInDepartmentAsync(int providerId, int departmentId, int dayOfWeek, TimeSpan StartTime, TimeSpan EndTime)
        {
            return await _dbSet
                .AnyAsync(ds => ds.ProviderId == providerId && ds.DepartmentId == departmentId && ds.DayOfWeek == dayOfWeek
                    && ((ds.StartTime < EndTime && ds.EndTime > StartTime)));
        }

    }
}
