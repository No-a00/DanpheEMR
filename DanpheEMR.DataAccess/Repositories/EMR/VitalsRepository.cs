using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.EMR;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.EMR
{
    public class VitalsRepository : GenericRepository<Vitals>, IVitalsRepository
    {
       

        public VitalsRepository(ApplicationDbContext context) : base(context) { }
        public async Task<IEnumerable<Vitals>> GetByVisitIdAsync(Guid visitId)
        {
            return await _dbSet.AsNoTracking()
                .Where(v => v.VisitId == visitId && !v.IsDeleted) 
                .OrderByDescending(v => v.RecordedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vitals>> GetHistoryByPatientIdAsync(Guid patientId)
        {
            return await _dbSet.AsNoTracking()
                .Where(v => v.PatientId == patientId && !v.IsDeleted) 
                .OrderByDescending(v => v.RecordedAt)
                .ToListAsync();
        }
        public async Task<Vitals?> GetLatestVitalsByPatientIdAsync(Guid patientId)
        {
            return await _dbSet.AsNoTracking()
                .Where(v => v.PatientId == patientId && !v.IsDeleted)
                .OrderByDescending(v => v.RecordedAt)
                .FirstOrDefaultAsync();
        }
    }
}