
using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.EMR;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.EMR
{
    public class PrescriptionItemRepository: IPrescriptionItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<PrescriptionItem> _dbSet;
        public PrescriptionItemRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<PrescriptionItem>();
        }
        public async Task<PrescriptionItem?> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task<PrescriptionItem> AddAsync(PrescriptionItem item)
        {
            await _dbSet.AddAsync(item);
            return item;
        }


        public Task UpdateAsync(PrescriptionItem item)
        {
            _dbSet.Update(item);
            return Task.CompletedTask;
        }
        public async Task CancelItemAsync(int id, string cancelReason,int userIdCancel)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;
            result.CancelReason = cancelReason;
            result.IsActive = false;
            result.UserIdCancel = userIdCancel;
        }
        //  Lấy toàn bộ các loại thuốc CỦA MỘT TỜ ĐƠN cụ thể
        public async Task<IEnumerable<PrescriptionItem>> GetItemsByPrescriptionIdAsync(int prescriptionId)
        {
            return await _dbSet.AsNoTracking().Where(p=>p.PrescriptionId == prescriptionId&&p.IsActive).ToListAsync();
        }

        // Thống kê truy vết: Xem một loại thuốc (ItemId) đã được kê trong những đơn nào
        // (Cực kỳ hữu ích khi có lệnh thu hồi thuốc khẩn cấp, bệnh viện cần tìm ngay những ai đã được kê loại thuốc này)
        public async Task<IEnumerable<PrescriptionItem>> GetItemsByDrugIdAsync(int itemId)
        {
            return await _dbSet.AsNoTracking().Where(p=>p.ItemId==itemId&&p.IsActive).ToListAsync();
        }
    }
}
