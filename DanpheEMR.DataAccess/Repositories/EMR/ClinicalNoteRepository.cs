using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interfaces.EMR;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.EMR
{
    public class ClinicalNoteRepository :GenericRepository<ClinicalNote>, IClinicalNoteRepository
    {

        public ClinicalNoteRepository(ApplicationDbContext context) : base(context) { }

        // Gộp lọc để tránh gọi GetAll() làm sập server
        public async Task<IEnumerable<ClinicalNote>> SearchNotesAsync(ClinicalNoteFilter filter)
        {
            
            IQueryable<ClinicalNote> query = _dbSet.AsNoTracking();

            if (filter != null)
            {
                if (filter.PatientId.HasValue)
                {
                    query = query.Where(x => x.PatientId == filter.PatientId.Value);
                }

                if (filter.ProviderId.HasValue)
                {
                    query = query.Where(x => x.ProviderId == filter.ProviderId.Value);
                }

                // Lọc theo ngày tạo Ghi chú lâm sàng
                if (filter.FromDate.HasValue)
                {
                    query = query.Where(x => x.NoteDate >= filter.FromDate.Value);
                }

                if (filter.ToDate.HasValue)
                {
                    var endOfDay = filter.ToDate.Value.Date.AddDays(1).AddTicks(-1);
                    query = query.Where(x => x.NoteDate <= endOfDay);
                }
            }

            // Sắp xếp: Ghi chú mới nhất hiển thị lên đầu (Rất quan trọng cho Bác sĩ)
            query = query.OrderByDescending(x => x.NoteDate);

            return await query.ToListAsync();
        }
        public async Task<ClinicalNote?> GetNoteByVisitIdAsync(Guid visitId)
        {

            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(c => c.VisitId == visitId && !c.IsDeleted);
        }
    }
}