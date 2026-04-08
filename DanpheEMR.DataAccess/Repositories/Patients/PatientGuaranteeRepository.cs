using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Patients;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Patients
{
    public class PatientGuaranteeRepository : GenericRepository<PatientGuarantee>, IPatientGuaranteeRepository
    {
        public PatientGuaranteeRepository(ApplicationDbContext context) : base(context) { }
        public async Task<IEnumerable<PatientGuarantee>> GetAllGuaranteesByPatientIdAsync(Guid patientId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.PatientId == patientId)
                .ToListAsync();
        }
        public async Task<IEnumerable<PatientGuarantee>> GetActiveGuaranteesByPatientIdAsync(Guid patientId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.PatientId == patientId && !p.IsDeleted)
                .ToListAsync();
        }
        public async Task<IEnumerable<PatientGuarantee>> GetGuaranteedPatientsByIdCardAsync(string idCardNumber)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.IDCardNumber == idCardNumber && !p.IsDeleted)
                .ToListAsync();
        }
    }
}