using DanpheEMR.Core.Domain.Appointment;
using DanpheEMR.Core.Iterface.Base;

namespace DanpheEMR.Core.Iterfaces.Appointment
{
    public interface IServiceCategoryRepository : IGenericRepository<ServiceCategory>
    {
        //  Lấy thông tin nhóm dựa vào Mã nhóm (CategoryCode)
        Task<ServiceCategory> GetByCategoryCodeAsync(string categoryCode);

        //  Kiểm tra xem Mã nhóm (CategoryCode) đã tồn tại chưa?
        Task<bool> IsCodeExistsAsync(string categoryCode);

        // Lấy ra danh sách các Nhóm dịch vụ, KÈM THEO tất cả các Dịch vụ (ServiceItem) bên trong nó
        Task<IEnumerable<ServiceCategory>> GetAllWithItemsAsync();
    }
}
