using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Pharmacy
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {

        // Tìm kiếm nhà cung cấp theo tên hoặc mã số thuế
        Task<IEnumerable<Supplier>> SearchSuppliersAsync(string keyword);

        // Chỉ lấy những Nhà cung cấp ĐANG HỢP TÁC (Dùng để load Dropdown khi làm Phiếu nhập kho)
        Task<IEnumerable<Supplier>> GetActiveSuppliersAsync();
    }
}