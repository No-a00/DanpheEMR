
using DanpheEMR.Core.Domain.Pharmacy;

namespace DanpheEMR.Core.Interface.Pharmacy
{
    public interface IStockTransactionRepository
    {
        // Hệ thống sẽ tự động gọi hàm này mỗi khi có 1 giao dịch Xuất/Nhập xảy ra
        Task<StockTransaction> AddAsync(StockTransaction transaction);
        Task<StockTransaction> GetByIdAsync(int id);
        // Xem Thẻ kho của 1 loại thuốc cụ thể (Ví dụ: Giám đốc muốn xem biến động của Paracetamol trong tháng này)
        Task<IEnumerable<StockTransaction>> GetTransactionsByItemAsync(int itemId, DateTime fromDate, DateTime toDate);
        // Xem toàn bộ lịch sử xuất/nhập của 1 Kho cụ thể trong ngày hôm nay
        Task<IEnumerable<StockTransaction>> GetTransactionsByStoreAsync(int storeId, DateTime date);
        // Truy vết nguồn gốc: Tìm tất cả giao dịch liên quan đến 1 mã chứng từ
        Task<IEnumerable<StockTransaction>> GetTransactionsByReferenceNoAsync(string referenceNo);
    }
}