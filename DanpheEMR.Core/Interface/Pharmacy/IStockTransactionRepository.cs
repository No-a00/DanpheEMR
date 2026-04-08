
using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Pharmacy
{
    public interface IStockTransactionRepository : IGenericRepository<StockTransaction>
    {
        // Xem Thẻ kho của 1 loại thuốc cụ thể (Ví dụ: Giám đốc muốn xem biến động của Paracetamol trong tháng này)
        Task<IEnumerable<StockTransaction>> GetTransactionsByItemAsync(Guid itemId, DateTime fromDate, DateTime toDate);
        // Xem toàn bộ lịch sử xuất/nhập của 1 Kho cụ thể trong ngày hôm nay
        Task<IEnumerable<StockTransaction>> GetTransactionsByStoreAsync(Guid storeId, DateTime date);
        // Truy vết nguồn gốc: Tìm tất cả giao dịch liên quan đến 1 mã chứng từ
        Task<IEnumerable<StockTransaction>> GetTransactionsByReferenceNoAsync(string referenceNo);
    }
}