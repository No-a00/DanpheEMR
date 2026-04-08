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
        public async Task<List<DoctorSchedule>> GetSchedulesByProviderIdAsync(Guid providerId, DateTime date)
        {
            var targetDayOfWeek = date.DayOfWeek;

            return await _dbSet
                .Where(ds => ds.ProviderId == providerId && ds.DayOfWeek == targetDayOfWeek)
                .ToListAsync();
        }
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