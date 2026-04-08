using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Patients;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Patients
{
    public class DischargeRepository : GenericRepository<Discharge>, IDischargeRepository
    {
        public DischargeRepository(ApplicationDbContext context) : base(context) { }
        public async Task<Discharge?> GetByAdmissionIdAsync(Guid admissionId)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(d => d.AdmissionId == admissionId && !d.IsDeleted );
        }

        // Lấy lịch sử các lần ra viện của bệnh nhân
        public async Task<IEnumerable<Discharge>> GetDischargesByPatientIdAsync(Guid patientId)
        {
            return await _dbSet.AsNoTracking()
                .Where(d => d.PatientId == patientId && !d.IsDeleted )
                .OrderByDescending(d => d.DischargeDate) 
                .ToListAsync();
        }

        public async Task<IEnumerable<Discharge>> GetDischargesByDateAsync(DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = date.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()
                .Where(d => d.DischargeDate >= startOfDay
                         && d.DischargeDate <= endOfDay
                         && !d.IsDeleted)
                .ToListAsync();
        }
        public async Task<IEnumerable<Discharge>> GetDischargesByConditionAsync(string condition, DateTime fromDate, DateTime toDate)
        {
            var endOfDay = toDate.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()
                .Where(d => d.DischargeCondition == condition
                         && d.DischargeDate >= fromDate
                         && d.DischargeDate <= endOfDay
                         && !d.IsDeleted)
                .OrderByDescending(d => d.DischargeDate)
                .ToListAsync();
        }
    }
}