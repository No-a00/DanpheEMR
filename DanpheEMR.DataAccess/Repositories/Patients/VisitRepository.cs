using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Patients;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Patients
{
    public class VisitRepository : IVisitRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Visit> _dbSet;

        public VisitRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Visit>();
        }

        public async Task<Visit?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Visit> AddAsync(Visit visit)
        {
            await _dbSet.AddAsync(visit);
            return visit;
        }

        public Task UpdateAsync(Visit visit)
        {
            _dbSet.Update(visit);
            return Task.CompletedTask;
        }

        public async Task CancelVisitAsync(Guid id, string cancelReason, Guid userIdCancel)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.CancelReason = cancelReason;
            result.UserIdCancel = userIdCancel;
        }

        //  Lấy danh sách bệnh nhân khám của 1 Bác sĩ trong ngày
        public async Task<IEnumerable<Visit>> GetActiveVisitsByProviderAsync(Guid providerId, DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = date.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()
                .Where(v => v.ProviderId == providerId
                         && v.VisitDate >= startOfDay
                         && v.VisitDate <= endOfDay
                         && v.IsActive == true)
                .OrderBy(v => v.VisitDate) 
                .ToListAsync();
        }

        // Xem lịch sử các lần đến khám của bệnh nhân
        public async Task<IEnumerable<Visit>> GetVisitsByPatientIdAsync(Guid patientId)
        {
            return await _dbSet.AsNoTracking()
                .Where(v => v.PatientId == patientId && v.IsActive == true)
                .OrderByDescending(v => v.VisitDate) 
                .ToListAsync();
        }

        //  Dùng khi Bác sĩ bấm nút "In Hồ Sơ Bệnh Án"
        public async Task<Visit?> GetVisitWithAllDetailsAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Include(v => v.Patient)      // Lấy thông tin Bệnh nhân (Tên, Tuổi, Giới tính...) để in lên header
                .Include(v => v.Department)   // Lấy tên Phòng/Khoa khám
                .Include(v => v.Provider)     // Lấy tên Bác sĩ khám để in xuống chữ ký cuối trang
                .Include(v => v.Vitals)       // Kéo theo Mạch, Nhiệt độ, Huyết áp, BMI
                .Include(v => v.ClinicalNotes)// Kéo theo Ghi chú quá trình khám của Bác sĩ
                .Include(v => v.Diagnoses)    // Kéo theo Kết luận chẩn đoán (Mã ICD-10)
                .Include(v => v.DoctorOrders) // Kéo theo danh sách Cận lâm sàng (Siêu âm, X-Quang, Xét nghiệm máu)
                .Include(v => v.Prescriptions)// Kéo theo Đơn thuốc đã kê để bệnh nhân cầm đi mua
                .FirstOrDefaultAsync(v => v.Id == id && v.IsActive == true);
        }
    }
}