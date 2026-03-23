using DanpheEMR.Core.Domain.EMR;
// Nhớ thêm using Interface của bạn vào nhé (tôi giả định đường dẫn như dưới)
using DanpheEMR.Core.Interface.EMR;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.EMR
{
    // Đã thêm : IPrescriptionRepository
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Prescription> _dbSet;

        public PrescriptionRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Prescription>();
        }

        public async Task<Prescription?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Prescription?> GetPrescriptionWithItemsAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Include(p => p.Items) // Tên Collection Items của bạn quá chuẩn!
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Prescription> AddAsync(Prescription prescription)
        {
            await _dbSet.AddAsync(prescription);
            return prescription;
        }

        public Task UpdateAsync(Prescription prescription)
        {
            _dbSet.Update(prescription);
            return Task.CompletedTask;
        }

        public async Task CancelPrescriptionAsync(Guid prescriptionId, string cancelReason, Guid userIdCancel)
        {
            var result = await _dbSet.FindAsync(prescriptionId);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.CancelReason = cancelReason;
            result.UserIdCancel = userIdCancel;
        }

        //  Mở hồ sơ bệnh án của ngày hôm nay lên xem có kê đơn gì không
        public async Task<IEnumerable<Prescription>> GetPrescriptionsByVisitIdAsync(Guid visitId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.VisitId == visitId && p.IsActive == true)
                .ToListAsync();
        }

        //  Xem lại toàn bộ lịch sử uống thuốc của bệnh nhân từ trước đến nay
        public async Task<IEnumerable<Prescription>> GetPrescriptionsByPatientIdAsync(Guid patientId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.PatientId == patientId && p.IsActive == true)
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
                         && p.IsActive == true)
                .ToListAsync();
        }

        //  Dược sĩ lọc các đơn "Active/Pending" để gọi tên bệnh nhân ra nhận thuốc
        public async Task<IEnumerable<Prescription>> GetPrescriptionsByStatusAsync(string status)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.Status == status && p.IsActive == true)
                .ToListAsync();
        }
    }
}