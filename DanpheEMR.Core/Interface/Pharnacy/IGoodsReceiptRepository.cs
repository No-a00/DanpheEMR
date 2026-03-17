using DanpheEMR.Core.Domain.Pharmacy;
namespace DanpheEMR.Core.Interfaces.Pharnacy
{
    public interface IGoodsReceiptRepository
    {
        Task<GoodsReceipt> GetByIdAsync(int id);
        // Khi Kế toán kho tạo nháp phiếu nhập
        Task<GoodsReceipt> AddAsync(GoodsReceipt goodsReceipt);

        // Khi Trưởng khoa duyệt phiếu (Đổi Status từ Pending -> Approved)
        Task UpdateAsync(GoodsReceipt goodsReceipt);
        // Lấy danh sách các Phiếu chờ duyệt (Để hiện thông báo đỏ cho Trưởng khoa Dược)
        Task<IEnumerable<GoodsReceipt>> GetPendingReceiptsAsync();  
        // Thống kê: Lấy danh sách phiếu nhập trong 1 khoảng thời gian (Dùng để báo cáo công nợ với Nhà cung cấp)
        Task<IEnumerable<GoodsReceipt>> GetReceiptsByDateAsync(DateTime fromDate, DateTime toDate);
        // QUAN TRỌNG: Lấy Phiếu nhập tổng KÈM THEO toàn bộ các mặt hàng chi tiết (GoodsReceiptItem) bên trong
        Task<GoodsReceipt> GetReceiptWithItemsAsync(int id);
    }
}