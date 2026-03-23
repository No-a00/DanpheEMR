
using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.EMR;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.EMR
{
    public class MedicationAdministrationRepository: IMedicationAdministrationRepository  
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<MedicationAdministration> _dbSet;
        public MedicationAdministrationRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<MedicationAdministration>();
        }
        public async Task<MedicationAdministration?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

        }
        public async Task<MedicationAdministration> AddAsync(MedicationAdministration administration)
        {
            await _dbSet.AddAsync(administration);
            return administration;
        }
        public  Task UpdateAsync(MedicationAdministration administration)
        {
            _dbSet.Update(administration);
            return Task.CompletedTask; 
        }
        public async Task VoidAdministrationAsync(Guid id, string voidReason, int voidedByUserId)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null||result.IsActive== false) return;
            result.IsActive = false;
            result.VoidReason = voidReason;
            result.VoidedByUserId = voidedByUserId;
        }
        public async Task<IEnumerable<MedicationAdministration>> GetByAdmissionIdAsync(int admissionId)
        {
            return await _dbSet.AsNoTracking()    
                .Where(d => d.AdmissionId == admissionId && d.IsActive == false) 
                .OrderByDescending(d => d.AdministeredTime) 
                .ToListAsync();
        }
        // Kiểm tra xem một loại thuốc cụ thể (PrescriptionItemId) đã được cho uống/tiêm mấy lần rồi
        public async Task<IEnumerable<MedicationAdministration>> GetByPrescriptionItemIdAsync(int prescriptionItemId)
        {
            return await _dbSet.AsNoTracking()
                .Where(m => m.PrescriptionItemId == prescriptionItemId && m.IsActive == false)
                .OrderBy(m => m.AdministeredTime) // Sắp xếp từ sáng đến tối để dễ nhìn tiến độ
                .ToListAsync();
        }
        // Xem danh sách các loại thuốc mà 1 Y tá cụ thể đã thực hiện trong 1 ngày/ca trực
        public async Task<IEnumerable<MedicationAdministration>> GetByNurseIdAsync(int nurseId, DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = date.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()
                .Where(m => m.NurseId == nurseId
                         && m.AdministeredTime >= startOfDay
                         && m.AdministeredTime <= endOfDay
                         && m.IsActive == false)
                .ToListAsync();
        }
    }
}
