using DanpheEMR.Core.Domain.Billing;
using DanpheEMR.Core.Interface.Base;
namespace DanpheEMR.Core.Interfaces.Billing
{
    public interface IServiceItemRepository : IGenericRepository<ServiceItem>
    {
        //  Tìm kiếm nhanh theo Tên dịch vụ hoặc Mã dịch vụ (Dùng cho ô Search Autocomplete)
        Task<IEnumerable<ServiceItem>> SearchByNameOrCodeAsync(string keyword);

        //  Lọc danh sách Dịch vụ theo Nhóm (Ví dụ: Chỉ lấy các dịch vụ thuộc Khoa Xét Nghiệm)
        Task<IEnumerable<ServiceItem>> GetItemsByCategoryAsync(Guid categoryId);
        // Kiểm tra xem Mã dịch vụ (ItemCode) đã được sử dụng chưa? (Tránh tạo trùng mã)
        Task<bool> IsItemCodeExistsAsync(string itemCode, Guid? excludeId = null);
    }
}
