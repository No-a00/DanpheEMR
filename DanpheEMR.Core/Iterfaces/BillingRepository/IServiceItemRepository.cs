using DanpheEMR.Core.Domain.Appointment;
using DanpheEMR.Core.Iterface.Base;

namespace DanpheEMR.Core.Iterfaces.BillingRepository
{
    public interface IServiceItemRepository : IGenericRepository<ServiceItem>
    {
        //  Tìm kiếm nhanh theo Tên dịch vụ hoặc Mã dịch vụ (Dùng cho ô Search Autocomplete)
        Task<IEnumerable<ServiceItem>> SearchByNameOrCodeAsync(string keyword);

        //  Lọc danh sách Dịch vụ theo Nhóm (Ví dụ: Chỉ lấy các dịch vụ thuộc Khoa Xét Nghiệm)
        Task<IEnumerable<ServiceItem>> GetItemsByCategoryAsync(int categoryId);
        // Kiểm tra xem Mã dịch vụ (ItemCode) đã được sử dụng chưa? (Tránh tạo trùng mã)
        Task<bool> IsItemCodeExistsAsync(string itemCode, int? excludeId = null);
    }
}
