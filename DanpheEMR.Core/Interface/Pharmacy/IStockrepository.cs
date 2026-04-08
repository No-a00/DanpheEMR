using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Pharmacy 
{
    public interface IStockRepository : IGenericRepository<Stock>
    {
        
        // Trưởng khoa Dược muốn xem Thuốc A đang nằm ở những lô nào, kho nào, tổng tồn bao nhiêu?
        Task<IEnumerable<Stock>> GetStocksByItemIdAsync(Guid itemId);

        // Kiểm kê kho: Lấy toàn bộ danh sách tồn của một kho cụ thể (VD: Kho Ngoại Trú)
        Task<IEnumerable<Stock>> GetStocksByStoreIdAsync(Guid storeId);

        // CẢNH BÁO ĐỎ: Lấy danh sách các lô thuốc sắp hết hạn trong vòng X ngày tới (VD: 30 ngày)
        // Ghi chú lúc implement: Nhớ Where(AvailableQuantity > 0 && ExpiryDate <= DateTime.Now.AddDays(daysToThreshold))
        Task<IEnumerable<Stock>> GetExpiringStocksAsync(Guid storeId, int daysToThreshold);
    }
}