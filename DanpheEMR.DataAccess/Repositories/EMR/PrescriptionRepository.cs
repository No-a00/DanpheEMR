using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.EMR;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.EMR
{
    public class PrescriptionRepository : GenericRepository<Prescription>,IPrescriptionRepository
    {
  
        public PrescriptionRepository(ApplicationDbContext context) : base(context)
        {
        }

 
        public async Task<Prescription?> GetPrescriptionWithItemsAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Include(p => p.Items) 
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Prescription>> GetPrescriptionsByVisitIdAsync(Guid visitId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.VisitId == visitId && !p.IsDeleted)
                .ToListAsync();
        }

        //  Xem lại toàn bộ lịch sử uống thuốc của bệnh nhân từ trước đến nay
        public async Task<IEnumerable<Prescription>> GetPrescriptionsByPatientIdAsync(Guid patientId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.PatientId == patientId && !p.IsDeleted)
                .OrderByDescending(p => p.CreatedAt) // Đơn mới nhất phải nổi lên đầu
                .ToListAsync();
        }

        //  Bác sĩ muốn xem hôm nay mình đã kê bao nhiêu đơn
        public async Task<IEnumerable<Prescription>> GetPrescriptionsByPrescriberAsync(Guid prescriberId, DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = date.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()
                .Where(p => p.PrescriberId == prescriberId
                         && p.CreatedAt >= startOfDay
                         && p.CreatedAt <= endOfDay
                         && !p.IsDeleted)
                .ToListAsync();
        }

        //  Dược sĩ lọc các đơn "Active/Pending" để gọi tên bệnh nhân ra nhận thuốc
        public async Task<IEnumerable<Prescription>> GetPrescriptionsByStatusAsync(string status)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.Status == status && !p.IsDeleted)
                .ToListAsync();
        }
    }
}