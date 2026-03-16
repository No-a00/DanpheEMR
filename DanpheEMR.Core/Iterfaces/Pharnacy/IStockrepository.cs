using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanpheEMR.Core.Domain.Pharmacy 
{
    public interface IStockRepository
    {
        Task<Stock> GetByIdAsync(int id);
        Task<Stock> AddAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        // Lấy danh sách các lô CÒN HÀNG và CÒN HẠN của 1 loại thuốc trong 1 kho cụ thể
        // (Bắt buộc phải sắp xếp ExpiryDate tăng dần để ưu tiên xuất lô cận date trước)
        Task<IEnumerable<Stock>> GetAvailableStocksAsync(int itemId, int storeId);
        // Trưởng khoa Dược muốn xem Thuốc A đang nằm ở những lô nào, kho nào, tổng tồn bao nhiêu?
        Task<IEnumerable<Stock>> GetStocksByItemIdAsync(int itemId);

        // Kiểm kê kho: Lấy toàn bộ danh sách tồn của một kho cụ thể (VD: Kho Ngoại Trú)
        Task<IEnumerable<Stock>> GetStocksByStoreIdAsync(int storeId);

        // CẢNH BÁO ĐỎ: Lấy danh sách các lô thuốc sắp hết hạn trong vòng X ngày tới (VD: 30 ngày)
        // (Để khoa Dược kịp thời làm thủ tục trả hàng cho nhà cung cấp hoặc đem đi hủy)
        Task<IEnumerable<Stock>> GetExpiringStocksAsync(int storeId, int daysToThreshold);
    }
}