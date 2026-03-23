using DanpheEMR.Core.Domain.OT;
using DanpheEMR.Core.Interfaces.OT;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.OT
{
    public class OTRoomRepository : IOTRoomRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<OTRoom> _dbSet;

        public OTRoomRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<OTRoom>();
        }

        public async Task<OTRoom?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<OTRoom>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<OTRoom> AddAsync(OTRoom room)
        {
            await _dbSet.AddAsync(room);
            return room;
        }

        public Task UpdateAsync(OTRoom room)
        {
            _dbSet.Update(room);
            return Task.CompletedTask;
        }

        // Lấy danh sách các phòng ĐANG HOẠT ĐỘNG (IsAvailable = true)
        public async Task<IEnumerable<OTRoom>> GetAvailableRoomsAsync()
        {
            return await _dbSet.AsNoTracking().Where(o => o.IsAvailable).ToListAsync();
        }
    }
}