using DanpheEMR.Core.Domain.OT;
using DanpheEMR.Core.Enums; 
using DanpheEMR.Core.Interface.OT;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.OT
{
    public class OTScheduleRepository : GenericRepository<OTSchedule>, IOTScheduleRepository
    {
        public OTScheduleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OTSchedule>> GetSchedulesByDateAsync(DateTime date)
        {
            return await _context.Set<OTSchedule>()
                .Include(o => o.Patient)
                .Include(o => o.Surgeon)
                .Where(o => o.SurgeryDate.Date == date.Date && o.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<OTSchedule>> GetSchedulesByTypeAsync(string surgeryType)
        {
            return await _context.Set<OTSchedule>()
                .Include(o => o.Patient)
                .Where(o => o.SurgeryType.ToLower().Contains(surgeryType.ToLower()) && o.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<OTSchedule>> GetSchedulesBySurgeonAsync(Guid surgeonId, DateTime date)
        {
            return await _context.Set<OTSchedule>()
                .Include(o => o.Patient)
                .Include(o => o.OTRoom)
                .Where(o => o.SurgeonId == surgeonId && o.SurgeryDate.Date == date.Date && o.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<OTSchedule>> GetSchedulesByRoomAsync(Guid roomId, DateTime date)
        {
            return await _context.Set<OTSchedule>()
                .Include(o => o.Patient)
                .Include(o => o.Surgeon)
                .Where(o => o.OTRoomId == roomId && o.SurgeryDate.Date == date.Date && o.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> IsRoomAvailableAsync(Guid roomId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            bool isOccupied = await _context.Set<OTSchedule>()
                .AnyAsync(o =>
                    o.OTRoomId == roomId &&
                    o.SurgeryDate.Date == date.Date &&
                    o.IsActive &&
                    o.Status != OTStatus.Cancelled && 
                    (o.StartTime < endTime && o.EndTime > startTime)
                );

            return !isOccupied; 
        }
        public async Task<bool> IsSurgeonAvailableAsync(Guid surgeonId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            bool isOccupied = await _context.Set<OTSchedule>()
                .AnyAsync(o =>
                    o.SurgeonId == surgeonId &&
                    o.SurgeryDate.Date == date.Date &&
                    o.IsActive &&
                    o.Status != OTStatus.Cancelled &&
                    (o.StartTime < endTime && o.EndTime > startTime)
                );

            return !isOccupied;
        }
    }
}