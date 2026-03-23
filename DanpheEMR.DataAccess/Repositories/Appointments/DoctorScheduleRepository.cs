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

        // Tìm lịch làm việc khớp với thời gian bệnh nhân muốn đặt
        public async Task<DoctorSchedule?> GetDoctorScheduleAsync(Guid doctorId, DateTime appointmentDate)
        {
            var time = appointmentDate.TimeOfDay;

            return await _dbSet
                .FirstOrDefaultAsync(ds =>
                    ds.ProviderId == doctorId &&
                    ds.DayOfWeek.Date == appointmentDate.Date && // So sánh phần Ngày
                    ds.StartTime <= time &&
                    ds.EndTime > time);
        }

        // Lấy tất cả các khung giờ làm việc của bác sĩ trong 1 ngày
        public async Task<List<DoctorSchedule>> GetSchedulesByProviderIdAsync(Guid providerId, DateTime date)
        {
            return await _dbSet
                .Where(ds => ds.ProviderId == providerId && ds.DayOfWeek.Date == date.Date)
                .ToListAsync();
        }

        // Tìm 1 lịch cụ thể dựa trên mốc thời gian (Đã sửa lỗi chính tả và Guid)
        public async Task<DoctorSchedule?> GetScheduleByProviderIdAndTimeAsync(Guid providerId, DateTime date, TimeSpan time)
        {
            return await _dbSet
                .FirstOrDefaultAsync(ds =>
                    ds.ProviderId == providerId &&
                    ds.DayOfWeek.Date == date.Date &&
                    ds.StartTime <= time &&
                    ds.EndTime > time);
        }

        public async Task<bool> IsDoctorScheduledInDepartmentAsync(Guid providerId, Guid departmentId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            return await _dbSet
                .AnyAsync(ds =>
                    ds.ProviderId == providerId &&
                    ds.DepartmentId == departmentId &&
                    ds.DayOfWeek.Date == date.Date &&
                    ((ds.StartTime < endTime && ds.EndTime > startTime)));
        }
    }
}