
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Patients;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

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

        public async Task<string> GeneratePatientCodeAsync()
        {

            string currentYear = DateTime.UtcNow.ToString("yy");
            string prefix = $"BN{currentYear}"; 
            var lastPatient = await _dbSet
                .Where(p => p.PatientCode != null && p.PatientCode.StartsWith(prefix))
                .OrderByDescending(p => p.PatientCode)
                .FirstOrDefaultAsync();

            //  Nếu chưa có bệnh nhân nào trong năm nay -> Trả về số 0001
            if (lastPatient == null)
            {
                return $"{prefix}0001"; 
            }
            string lastSequenceStr = lastPatient.PatientCode.Substring(prefix.Length);

            //  Cộng thêm 1 và format lại thành 4 chữ số
            if (int.TryParse(lastSequenceStr, out int lastSequence))
            {
                int nextSequence = lastSequence + 1;
                return $"{prefix}{nextSequence.ToString("D4")}"; 
            }
            return $"{prefix}0001";
        }
    }
}


