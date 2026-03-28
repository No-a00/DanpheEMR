using DanpheEMR.Core.Domain.Pharmacy;

namespace DanpheEMR.Core.Interface.Pharmacy 
{
    public interface IStockRepository
    {
        Task<Stock?> GetByIdAsync(Guid Id);
        
        Task<Stock> AddAsync(Stock stock);
        
        // Gọi hàm Update khi có Phiếu Xuất (Dispatch), Bán lẻ (Sale), hoặc Điều chỉnh (Adjustment)
        // Hệ thống sẽ thay đổi cột AvailableQuantity rồi gọi Update
        Task UpdateAsync(Stock stock);

        // Lấy danh sách các lô CÒN HÀNG và CÒN HẠN của 1 loại thuốc trong 1 kho cụ thể
        // Ghi chú lúc implement: Nhớ Where(AvailableQuantity > 0 && ExpiryDate >= DateTime.Now)
        // (Bắt buộc phải sắp xếp ExpiryDate tăng dần để ưu tiên xuất lô cận date trước theo nguyên tắc FIFO/FEFO)
        Task<IEnumerable<Stock>> GetAvailableStocksAsync(Guid itemId, Guid storeId);
        
        // Trưởng khoa Dược muốn xem Thuốc A đang nằm ở những lô nào, kho nào, tổng tồn bao nhiêu?
        Task<IEnumerable<Stock>> GetStocksByItemIdAsync(Guid itemId);

        // Kiểm kê kho: Lấy toàn bộ danh sách tồn của một kho cụ thể (VD: Kho Ngoại Trú)
        Task<IEnumerable<Stock>> GetStocksByStoreIdAsync(Guid storeId);

        // CẢNH BÁO ĐỎ: Lấy danh sách các lô thuốc sắp hết hạn trong vòng X ngày tới (VD: 30 ngày)
        // Ghi chú lúc implement: Nhớ Where(AvailableQuantity > 0 && ExpiryDate <= DateTime.Now.AddDays(daysToThreshold))
        Task<IEnumerable<Stock>> GetExpiringStocksAsync(Guid storeId, int daysToThreshold);
    }
}