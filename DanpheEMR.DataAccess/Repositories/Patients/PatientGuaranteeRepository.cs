using DanpheEMR.Core.Domain.Patients;
// Nhớ using Interface của bạn vào nhé!
using DanpheEMR.Core.Interface.Patients;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Patients
{
    public class PatientGuaranteeRepository : IPatientGuaranteeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<PatientGuarantee> _dbSet;

        public PatientGuaranteeRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<PatientGuarantee>();
        }
        public async Task<PatientGuarantee?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PatientGuarantee> AddAsync(PatientGuarantee guarantee)
        {
            await _dbSet.AddAsync(guarantee);
            return guarantee;
        }

        public Task UpdateAsync(PatientGuarantee guarantee)
        {
            _dbSet.Update(guarantee);
            return Task.CompletedTask;
        }

        public async Task CancelGuaranteeAsync(Guid id, string cancelReason, int cancelledByUserId)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.CancelReason = cancelReason;
            result.CancelledByUserId = cancelledByUserId;
        }
        public async Task<IEnumerable<PatientGuarantee>> GetAllGuaranteesByPatientIdAsync(Guid patientId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.PatientId == patientId)
                .ToListAsync();
        }
        public async Task<IEnumerable<PatientGuarantee>> GetActiveGuaranteesByPatientIdAsync(Guid patientId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.PatientId == patientId && p.IsActive == true)
                // Gợi ý Nghiệp vụ: Nếu class của bạn có cột Ngày hết hạn (ExpiryDate), hãy thêm điều kiện này:
                // && p.ExpiryDate >= DateTime.Now 
                .ToListAsync();
        }
        public async Task<IEnumerable<PatientGuarantee>> GetGuaranteedPatientsByIdCardAsync(string idCardNumber)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.IDCardNumber == idCardNumber && p.IsActive == true)
                .ToListAsync();
        }
    }
}