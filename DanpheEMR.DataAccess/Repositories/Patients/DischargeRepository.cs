using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Patients;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Patients
{
    public class DischargeRepository : IDischargeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Discharge> _dbSet;

        public DischargeRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Discharge>();
        }
        public async Task<Discharge?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Discharge> AddAsync(Discharge discharge)
        {
            await _dbSet.AddAsync(discharge);
            return discharge;
        }

        public Task UpdateAsync(Discharge discharge)
        {
            _dbSet.Update(discharge);
            return Task.CompletedTask;
        }

        public async Task VoidDischargeAsync(Guid id, string voidReason, Guid voidedByUserId)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.VoidReason = voidReason;
            result.VoidedByUserId = voidedByUserId;
        }

        //  Lấy Giấy ra viện của một đợt nằm viện cụ thể 
        public async Task<Discharge?> GetByAdmissionIdAsync(Guid admissionId)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(d => d.AdmissionId == admissionId && d.IsActive == true);
        }

        // Lấy lịch sử các lần ra viện của bệnh nhân
        public async Task<IEnumerable<Discharge>> GetDischargesByPatientIdAsync(Guid patientId)
        {
            return await _dbSet.AsNoTracking()
                .Where(d => d.PatientId == patientId && d.IsActive == true)
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
                         && d.IsActive == true)
                .ToListAsync();
        }
        public async Task<IEnumerable<Discharge>> GetDischargesByConditionAsync(string condition, DateTime fromDate, DateTime toDate)
        {
            var endOfDay = toDate.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()
                .Where(d => d.DischargeCondition == condition
                         && d.DischargeDate >= fromDate
                         && d.DischargeDate <= endOfDay
                         && d.IsActive == true)
                .OrderByDescending(d => d.DischargeDate)
                .ToListAsync();
        }
    }
}