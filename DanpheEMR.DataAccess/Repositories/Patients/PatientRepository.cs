
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Patients;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Patients
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Patient> _dbSet;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Patient>();
        }

        public async Task<Patient?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        // Khi tạo mới, hệ thống sẽ tự động phát sinh PatientCode (VD: "PAT-2026-0001")
        // (Ghi chú nhỏ: Logic phát sinh mã này thường sẽ được viết ở tầng Service nhé)
        public async Task<Patient> AddAsync(Patient patient)
        {
            await _dbSet.AddAsync(patient);
            return patient;
        }

        public Task UpdateAsync(Patient patient)
        {
            _dbSet.Update(patient);
            return Task.CompletedTask;
        }

        public async Task DeactivateAsync(Guid id, string voidReason, int voidedByUserId)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.voidReason = voidReason;
            result.voidedByUserId = voidedByUserId;
        }

        // Tìm chính xác bằng Mã bệnh nhân (Khi bệnh nhân đưa thẻ cứng hoặc đọc mã)
        public async Task<Patient?> GetByPatientCodeAsync(string patientCode)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(p => p.PatientCode == patientCode && p.IsActive == true);
        }

        // Tìm kiếm linh hoạt: Gõ Tên, hoặc Số điện thoại, hoặc CMND/CCCD
        public async Task<IEnumerable<Patient>> SearchPatientsAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return new List<Patient>();

            keyword = keyword.Trim(); // Cắt khoảng trắng thừa do Lễ tân gõ nhầm

            return await _dbSet.AsNoTracking()
                .Where(p => p.IsActive == true &&
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
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive == true);
        }
    }
}