using DanpheEMR.Core.Domain.Appointments;
using DanpheEMR.Core.Interfaces.Appointment;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanpheEMR.DataAccess.Repositories.Appointments
{
    public class DoctorScheduleRepository : GenericRepository<DoctorSchedule>, IDoctorScheduleRepository
    {
        public DoctorScheduleRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<DoctorSchedule?> GetDoctorScheduleAsync(Guid doctorId, DateTime appointmentDate)
        {
            var time = appointmentDate.TimeOfDay;
            var targetDayOfWeek = appointmentDate.DayOfWeek; 

            return await _dbSet
                .FirstOrDefaultAsync(ds =>
                    ds.ProviderId == doctorId &&
                    ds.DayOfWeek == targetDayOfWeek && 
                    ds.StartTime <= time &&
                    ds.EndTime > time);
        }

        // 2. Lấy tất cả các khung giờ làm việc của bác sĩ trong 1 ngày
        public async Task<List<DoctorSchedule>> GetSchedulesByProviderIdAsync(Guid providerId, DateTime date)
        {
            var targetDayOfWeek = date.DayOfWeek;

            return await _dbSet
                .Where(ds => ds.ProviderId == providerId && ds.DayOfWeek == targetDayOfWeek)
                .ToListAsync();
        }

        // 3. Tìm 1 lịch cụ thể dựa trên mốc thời gian
        public async Task<DoctorSchedule?> GetScheduleByProviderIdAndTimeAsync(Guid providerId, DateTime date, TimeSpan time)
        {
            var targetDayOfWeek = date.DayOfWeek;

            return await _dbSet
                .FirstOrDefaultAsync(ds =>
                    ds.ProviderId == providerId &&
                    ds.DayOfWeek == targetDayOfWeek &&
                    ds.StartTime <= time &&
                    ds.EndTime > time);
        }

        // 4. Kiểm tra xem bác sĩ đã có lịch trong khoa vào khoảng thời gian đó chưa (Chống xếp trùng lịch làm việc)
        public async Task<bool> IsDoctorScheduledInDepartmentAsync(Guid providerId, Guid departmentId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var targetDayOfWeek = date.DayOfWeek;

            return await _dbSet
                .AnyAsync(ds =>
                    ds.ProviderId == providerId &&
                    ds.DepartmentId == departmentId &&
                    ds.DayOfWeek == targetDayOfWeek &&
                    (ds.StartTime < endTime && ds.EndTime > startTime));
        }
    }
}