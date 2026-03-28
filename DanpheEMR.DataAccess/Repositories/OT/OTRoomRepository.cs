using DanpheEMR.Core.Domain.OT;
using DanpheEMR.Core.Interface.OT;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanpheEMR.DataAccess.Repositories.OT
{
    public class OTRoomRepository : GenericRepository<OTRoom>, IOTRoomRepository
    {
        public OTRoomRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<OTRoom>> GetAvailableRoomsAsync()
        {
            return await _context.Set<OTRoom>()
                .Where(r => r.IsAvailable)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> IsRoomNameExistsAsync(string roomName, Guid? excludeId = null)
        {
            var query = _context.Set<OTRoom>().AsQueryable();
            if (excludeId.HasValue) query = query.Where(r => r.Id != excludeId.Value);
            return await query.AnyAsync(r => r.RoomName.ToLower() == roomName.ToLower());
        }
    }
}