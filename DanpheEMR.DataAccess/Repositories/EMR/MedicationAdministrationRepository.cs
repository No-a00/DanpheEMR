
using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.EMR;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.EMR
{
    public class MedicationAdministrationRepository: GenericRepository<MedicationAdministration>,IMedicationAdministrationRepository  
    {
        public MedicationAdministrationRepository(ApplicationDbContext context) : base(context) { }
        
    
        public async Task<IEnumerable<MedicationAdministration>> GetByAdmissionIdAsync(Guid admissionId)
        {
            return await _dbSet.AsNoTracking()    
                .Where(d => d.AdmissionId == admissionId && !d.IsDeleted) 
                .OrderByDescending(d => d.AdministeredTime) 
                .ToListAsync();
        }
       
        public async Task<IEnumerable<MedicationAdministration>> GetByPrescriptionItemIdAsync(Guid prescriptionItemId)
        {
            return await _dbSet.AsNoTracking()
                .Where(m => m.PrescriptionItemId == prescriptionItemId && !m.IsDeleted)
                .OrderBy(m => m.AdministeredTime) 
                .ToListAsync();
        }
        public async Task<IEnumerable<MedicationAdministration>> GetByNurseIdAsync(Guid nurseId, DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = date.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()
                .Where(m => m.NurseId == nurseId
                         && m.AdministeredTime >= startOfDay
                         && m.AdministeredTime <= endOfDay
                         && !m.IsDeleted)
                .ToListAsync();
        }
    }
}
