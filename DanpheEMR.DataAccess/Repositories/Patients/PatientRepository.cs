
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Patients;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Patients
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {


        public PatientRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Patient?> GetByPatientCodeAsync(string patientCode)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(p => p.PatientCode == patientCode && !p.IsDeleted);
        }
        public async Task<IEnumerable<Patient>> SearchPatientsAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return new List<Patient>();

            keyword = keyword.Trim(); 

            return await _dbSet.AsNoTracking()
                .Where(p => !p.IsDeleted == true &&
                           (p.FullName.Contains(keyword) ||
                            p.PhoneNumber.Contains(keyword) ||
                            p.IdCardNumber.Contains(keyword))) 
                .OrderByDescending(p => p.CreatedAt) 
                .ToListAsync();
        }
        public async Task<Patient?> GetPatientWithDetailsAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Include(p => p.Addresses) 
                .Include(p => p.Kins)   
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted == true);
        }
        public Task<bool> IsIdCardExistsAsync(string idCardNumber)
        {
            return _dbSet.AsNoTracking()
                .AnyAsync(p => p.IdCardNumber == idCardNumber && !p.IsDeleted);
        }

        public Task<string> GeneratePatientCodeAsync()
        {
            return Task.FromResult($"PT-{DateTime.UtcNow:yyyyMMddHHmmssfff}");
        }
    }
}