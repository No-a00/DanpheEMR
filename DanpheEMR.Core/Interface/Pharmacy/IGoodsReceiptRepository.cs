using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interfaces.Pharmacy 
{
    public interface IGoodsReceiptRepository : IGenericRepository<GoodsReceipt>
    {

        // Lấy danh sách các Phiếu chờ duyệt (Để hiện thông báo đỏ cho Trưởng khoa Dược)
        Task<IEnumerable<GoodsReceipt>> GetPendingReceiptsAsync();

        // Thống kê: Lấy danh sách phiếu nhập trong 1 khoảng thời gian (Dùng để báo cáo công nợ)
        Task<IEnumerable<GoodsReceipt>> GetReceiptsByDateAsync(DateTime fromDate, DateTime toDate);

        // QUAN TRỌNG: Lấy Phiếu nhập tổng KÈM THEO toàn bộ các mặt hàng chi tiết
        Task<GoodsReceipt?> GetReceiptWithItemsAsync(Guid Id); // Thêm dấu ?
    }
}