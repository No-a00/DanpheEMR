
using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.EMR;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.EMR
{
    public class PrescriptionItemRepository:GenericRepository<PrescriptionItem>, IPrescriptionItemRepository
    {
    
        public PrescriptionItemRepository(ApplicationDbContext context) : base(context) { }
   
        public async Task<IEnumerable<PrescriptionItem>> GetItemsByPrescriptionIdAsync(Guid prescriptionId)
        {
            return await _dbSet.AsNoTracking().Where(p=>p.PrescriptionId == prescriptionId&&!p.IsDeleted).ToListAsync();
        }

        // Thống kê truy vết: Xem một loại thuốc (ItemId) đã được kê trong những đơn nào
        // (Cực kỳ hữu ích khi có lệnh thu hồi thuốc khẩn cấp, bệnh viện cần tìm ngay những ai đã được kê loại thuốc này)
        public async Task<IEnumerable<PrescriptionItem>> GetItemsByDrugIdAsync(Guid itemId)
        {
            return await _dbSet.AsNoTracking().Where(p=>p.MedicineId==itemId&&!p.IsDeleted).ToListAsync();
        }
    }
}
