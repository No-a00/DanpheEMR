using DanpheEMR.Core.Domain.OT;
using DanpheEMR.Core.Interface.OT;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.OT
{
    public class OTScheduleRepository : IOTScheduleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<OTSchedule> _dbSet;

        public OTScheduleRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<OTSchedule>();
        }

        public async Task<OTSchedule?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<OTSchedule> AddAsync(OTSchedule schedule)
        {
            await _dbSet.AddAsync(schedule);
            return schedule;
        }

        // SỬA LỖI: Bỏ async, dùng Task.CompletedTask
        public Task UpdateAsync(OTSchedule schedule)
        {
            _dbSet.Update(schedule);
            return Task.CompletedTask;
        }

        public async Task CancelScheduleAsync(Guid id, string cancelReason, Guid cancelledByUserId)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.CancelReason = cancelReason;
            result.CancelledByUserId = cancelledByUserId;
        }

        public async Task<IEnumerable<OTSchedule>> GetSchedulesByDateAsync(DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = date.Date.AddDays(1).AddTicks(-1);
            return await _dbSet.AsNoTracking()
                .Where(x => x.SurgeryDate >= startOfDay && x.SurgeryDate <= endOfDay && x.IsActive)
                .OrderBy(x => x.StartTime) // Thêm sắp xếp giờ mổ từ sáng đến chiều
                .ToListAsync();
        }

        // Lọc danh sách theo Loại phẫu thuật
        public async Task<IEnumerable<OTSchedule>> GetSchedulesByTypeAsync(string surgeryType)
        {
            return await _dbSet.AsNoTracking()
                .Where(x => x.SurgeryType == surgeryType && x.IsActive)
                .OrderByDescending(x => x.SurgeryDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<OTSchedule>> GetSchedulesBySurgeonAsync(Guid surgeonId, DateTime date)
        {
            var targetDate = date.Date;

            return await _dbSet.AsNoTracking()
                .Where(x => x.SurgeonId == surgeonId && x.SurgeryDate.Date == targetDate && x.IsActive)
                .OrderBy(x => x.StartTime) 
                .ToListAsync();
        }

        public async Task<IEnumerable<OTSchedule>> GetSchedulesByRoomAsync(Guid roomId, DateTime date)
        {
            var targetDate = date.Date;
            return await _dbSet.AsNoTracking()
                .Where(x => x.OTRoomId == roomId && x.SurgeryDate.Date == targetDate && x.IsActive)
                .OrderBy(x => x.StartTime)
                .ToListAsync();
        }

        public async Task<bool> IsRoomAvailableAsync(Guid roomId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var targetDate = date.Date;
            var hasOverlap = await _dbSet.AsNoTracking()
                .AnyAsync(x => x.OTRoomId == roomId
                            && x.SurgeryDate.Date == targetDate
                            && x.IsActive
                            && x.StartTime < endTime 
                            && x.EndTime > startTime); 
            return !hasOverlap;
        }
    }
}