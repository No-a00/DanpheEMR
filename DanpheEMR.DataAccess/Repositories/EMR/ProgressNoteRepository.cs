using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.EMR;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.EMR
{
    public class ProgressNoteRepository : GenericRepository<ProgressNote>, IProgressNoteRepository
    {
     
        public ProgressNoteRepository(ApplicationDbContext context) : base(context) { }
        
    
        public async Task<IEnumerable<ProgressNote>> GetNotesByAdmissionIdAsync(Guid admissionId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.AdmissionId == admissionId && !p.IsDeleted)
                .OrderByDescending(p => p.NoteDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<ProgressNote>> GetNotesByProviderAsync(Guid providerId, DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = date.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()
                .Where(p => p.ProviderId == providerId
                         && p.CreatedAt >= startOfDay
                         && p.CreatedAt <= endOfDay
                         && !p.IsDeleted) 
                .ToListAsync();
        }
    }
}