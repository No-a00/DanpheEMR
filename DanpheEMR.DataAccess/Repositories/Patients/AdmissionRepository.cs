using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Patients;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using DanpheEMR.Core.Enums;


namespace DanpheEMR.DataAccess.Repositories.Patients
{
    public class AdmissionRepository : GenericRepository<Admission>, IAdmissionRepository
    {
        public AdmissionRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Admission>> GetActiveAdmissionsAsync()
        {
            return await _context.Set<Admission>()
                .Include(a => a.Patient)
                .Include(a => a.AdmittingDoctor)
                
                .Where(a => !a.IsDeleted && a.Status == AdmissionStatus.Active)
                .OrderByDescending(a => a.AdmissionDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Admission>> GetAdmissionsByPatientIdAsync(Guid patientId)
        {
            return await _context.Set<Admission>()
                .Include(a => a.AdmittingDoctor)
                .Include(a => a.Discharge)
                .Where(a => a.PatientId == patientId)
                .OrderByDescending(a => a.AdmissionDate)
                .AsNoTracking()
                .ToListAsync();
        }

    
        public async Task<Admission?> GetAdmissionWithTransfersAsync(Guid admissionId)
        {
            return await _context.Set<Admission>()
                .Include(a => a.Patient)
                .Include(a => a.Transfers)
                .Include(a => a.Discharge)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == admissionId);
        }
    }
}