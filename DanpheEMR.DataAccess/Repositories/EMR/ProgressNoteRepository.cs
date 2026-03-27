using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.EMR;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.EMR
{
    public class ProgressNoteRepository : IProgressNoteRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ProgressNote> _dbSet;

        public ProgressNoteRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<ProgressNote>();
        }

        public async Task<ProgressNote?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProgressNote> AddAsync(ProgressNote note)
        {
            await _dbSet.AddAsync(note);
            return note;
        }

        public Task UpdateAsync(ProgressNote note)
        {
            _dbSet.Update(note);
            return Task.CompletedTask;
        }

        public async Task VoidNoteAsync(Guid id, string voidReason, Guid voidedByUserId)
        {
            var result = await _dbSet.FindAsync(id);       
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.VoidReason = voidReason;
            result.VoidedByUserId = voidedByUserId;
        }
        public async Task<IEnumerable<ProgressNote>> GetNotesByAdmissionIdAsync(Guid admissionId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.AdmissionId == admissionId && p.IsActive == true)
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
                         && p.IsActive == true) 
                .ToListAsync();
        }
    }
}