using DanpheEMR.Core.Domain.Pharmacy;

namespace DanpheEMR.Core.Interfaces.Pharmacy // Đã sửa lỗi chính tả Pharnacy -> Pharmacy
{
    public interface IGoodsReceiptRepository
    {
        // Thêm dấu ? vì có thể tìm không ra phiếu
        Task<GoodsReceipt?> GetByIdAsync(Guid Id);

        // Khi Kế toán kho tạo nháp phiếu nhập
        Task<GoodsReceipt> AddAsync(GoodsReceipt goodsReceipt);

        // Khi Trưởng khoa duyệt phiếu (Đổi Status từ Pending -> Approved)
        Task UpdateAsync(GoodsReceipt goodsReceipt);

        // --- ĐÃ THÊM: Cực kỳ quan trọng để truy vết kho ---
        // Hủy phiếu nhập (Soft Delete) khi phát hiện sai sót hóa đơn, sai nhà cung cấp
        Task CancelReceiptAsync(Guid Id, string cancelReason, int cancelledByUserId);

        // Lấy danh sách các Phiếu chờ duyệt (Để hiện thông báo đỏ cho Trưởng khoa Dược)
        Task<IEnumerable<GoodsReceipt>> GetPendingReceiptsAsync();

        // Thống kê: Lấy danh sách phiếu nhập trong 1 khoảng thời gian (Dùng để báo cáo công nợ)
        Task<IEnumerable<GoodsReceipt>> GetReceiptsByDateAsync(DateTime fromDate, DateTime toDate);

        // QUAN TRỌNG: Lấy Phiếu nhập tổng KÈM THEO toàn bộ các mặt hàng chi tiết
        Task<GoodsReceipt?> GetReceiptWithItemsAsync(Guid Id); // Thêm dấu ?
    }
}