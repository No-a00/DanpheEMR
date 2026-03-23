
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Patients;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Patients
{
    public class AdmissionRepository : IAdmissionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Admission> _dbSet;

        public AdmissionRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Admission>();
        }

        public async Task<Admission?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Admission> AddAsync(Admission admission)
        {
            await _dbSet.AddAsync(admission);
            return admission;
        }

        public Task UpdateAsync(Admission admission)
        {
            _dbSet.Update(admission);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Admission>> GetActiveAdmissionsAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(a => a.Status == AdmissionStatus.Active && a.IsActive == true)
                .OrderByDescending(a => a.AdmissionDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<Admission>> GetAdmissionsByPatientIdAsync(Guid patientId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.PatientId == patientId && p.IsActive == true)
                .OrderByDescending(p => p.AdmissionDate)
                .ToListAsync();
        }
        public async Task<Admission?> GetAdmissionWithTransfersAsync(Guid admissionId)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == admissionId && a.IsActive == true);
        }
    }
}