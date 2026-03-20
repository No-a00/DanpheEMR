using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.EMR;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.EMR
{
    public class VitalsRepository : IVitalsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Vitals> _dbSet;

        public VitalsRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Vitals>();
        }

        public async Task<Vitals?> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Vitals> AddAsync(Vitals vitals)
        {
            await _dbSet.AddAsync(vitals);
            return vitals;
        }

        public Task UpdateAsync(Vitals vitals)
        {
            _dbSet.Update(vitals);
            return Task.CompletedTask;
        }

        // Sinh hiệu đo sai thì đánh dấu Hủy (Không xóa vật lý)
        public async Task VoidVitalsAsync(int id, string voidReason, int voidedByUserId)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.voidReason = voidReason;
            result.voidedByUserId = voidedByUserId;
        }

        // Lấy danh sách các lần đo sinh hiệu CỦA MỘT LƯỢT KHÁM
        public async Task<IEnumerable<Vitals>> GetByVisitIdAsync(int visitId)
        {
            return await _dbSet.AsNoTracking()
                .Where(v => v.VisitId == visitId && v.IsActive) 
                .OrderByDescending(v => v.RecordedAt)
                .ToListAsync();
        }

        // Lấy toàn bộ lịch sử sinh hiệu của Bệnh nhân qua các năm
        public async Task<IEnumerable<Vitals>> GetHistoryByPatientIdAsync(int patientId)
        {
            return await _dbSet.AsNoTracking()
                .Where(v => v.PatientId == patientId && v.IsActive) 
                .OrderByDescending(v => v.RecordedAt)
                .ToListAsync();
        }

        // Lấy nhanh chỉ số Sinh hiệu MỚI NHẤT của bệnh nhân
        public async Task<Vitals?> GetLatestVitalsByPatientIdAsync(int patientId)
        {
            return await _dbSet.AsNoTracking()
                .Where(v => v.PatientId == patientId && v.IsActive)
                .OrderByDescending(v => v.RecordedAt)
                .FirstOrDefaultAsync();
        }
    }
}