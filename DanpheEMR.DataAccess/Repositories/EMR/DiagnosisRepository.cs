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

        public async Task<Diagnosis?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Include(d => d.Provider)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<Diagnosis> AddAsync(Diagnosis diagnosis)
        {
            await _dbSet.AddAsync(diagnosis);
            return diagnosis;
        }

        public async Task UpdateAsync(Diagnosis diagnosis)
        {
            _dbSet.Update(diagnosis);
            await Task.CompletedTask;
        }

        public async Task VoidDiagnosisAsync(Guid diagnosisId, string reason, Guid voidedBy)
        {
            var diagnosis = await _dbSet.FindAsync(diagnosisId);
            if (diagnosis != null)
            {
                diagnosis.IsDeleted = true;
                diagnosis.Reason = reason;
                diagnosis.VoidedBy = voidedBy;
                _dbSet.Update(diagnosis);
            }
        }

        public async Task<IEnumerable<Diagnosis>> GetDiagnosesByVisitIdAsync(Guid visitId)
        {
            return await _dbSet.AsNoTracking()
                .Include(d => d.Provider)
                .Where(d => d.VisitId == visitId && !d.IsDeleted)
                .OrderByDescending(d => d.DiagnosisDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Diagnosis>> GetDiagnosesByPatientIdAsync(Guid patientId)
        {
            return await _dbSet.AsNoTracking()
                .Include(d => d.Provider) 
                .Where(d => d.PatientId == patientId && !d.IsDeleted)
                .OrderByDescending(d => d.DiagnosisDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Diagnosis>> GetDiagnosesByICD10Async(string icd10Code, DateTime fromDate, DateTime toDate)
        {
            var endOfDay = toDate.Date.AddDays(1).AddTicks(-1);
            return await _dbSet.AsNoTracking()
                .Include(d => d.Patient) 
                .Where(d => d.ICD10Code == icd10Code
                         && d.DiagnosisDate >= fromDate
                         && d.DiagnosisDate <= endOfDay
                         && !d.IsDeleted)
                .ToListAsync();
        }
    }
}