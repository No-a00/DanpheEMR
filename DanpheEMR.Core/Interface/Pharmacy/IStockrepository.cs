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
        // Kiểm tra xem kho còn đủ hàng để xuất/chuyển hay không
        Task<bool> CheckStockAvailabilityAsync(Guid storeId, Guid itemId, string batchNo, int requiredQuantity);
        // Trừ số lượng tồn kho (Dùng khi xuất/bán thuốc hoặc chuyển kho)
        Task DeductStockAsync(Guid storeId, Guid itemId, string batchNo, int quantity);
        // Lấy ngày hết hạn của một lô thuốc cụ thể trong kho
        Task<DateTime> GetExpiryDateAsync(Guid storeId, Guid itemId, string batchNo);

        // Cộng tồn kho (Tự động nhận diện: Nếu đã có lô thì cộng dồn, chưa có thì tạo mới)
        Task AddStockAsync(Guid storeId, Guid itemId, string batchNo, DateTime expiryDate, int quantity);
    }
}