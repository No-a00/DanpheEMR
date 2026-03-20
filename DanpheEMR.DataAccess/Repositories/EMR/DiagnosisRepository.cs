using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.EMR;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.EMR
{
    public class DiagnosisRepository : IDiagnosisRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Diagnosis> _dbSet;

        public DiagnosisRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Diagnosis>();
        }

        public async Task<Diagnosis?> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Diagnosis> AddAsync(Diagnosis diagnosis)
        {
            await _dbSet.AddAsync(diagnosis);
            return diagnosis;
        }
        public Task UpdateAsync(Diagnosis diagnosis)
        {
            _dbSet.Update(diagnosis);
            return Task.CompletedTask;
        }

        public async Task VoidDiagnosisAsync(int diagnosisId, string reason,int VoidedBy)
        {
            var result = await _dbSet.FindAsync(diagnosisId);
            if (result != null)
            {
                result.isDelete = true;
                result.reason = reason;
                result.VoidedBy = VoidedBy;
            }
        }

        public async Task<IEnumerable<Diagnosis>> GetDiagnosesByVisitIdAsync(int visitId)
        {
            return await _dbSet.AsNoTracking()
                .Where(d => d.VisitId == visitId && d.isDelete == false) 
                .ToListAsync();
        }

        /
        public async Task<IEnumerable<Diagnosis>> GetDiagnosesByICD10Async(string icd10Code, DateTime fromDate, DateTime toDate)
        {
            var endOfDay = toDate.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()
                .Where(d => d.ICD10Code == icd10Code
                         && d.CreatedAt >= fromDate
                         && d.CreatedAt <= endOfDay // Đã sửa fromDate thành endOfDay
                         && d.isDelete == false)    // Không lấy các ca chẩn đoán sai/đã hủy
                .ToListAsync();
        }
    }
}