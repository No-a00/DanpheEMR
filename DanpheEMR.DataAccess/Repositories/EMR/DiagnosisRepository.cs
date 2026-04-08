using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.EMR;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.EMR
{
    public class DiagnosisRepository :GenericRepository<Diagnosis>, IDiagnosisRepository
    {
        public DiagnosisRepository(ApplicationDbContext context) : base(context)
        {
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