using DanpheEMR.Core.Domain.Pharmacy;

namespace DanpheEMR.Core.Interface.Pharmacy
{
    public interface ISupplierRepository
    {
        Task<Supplier?> GetByIdAsync(int id);

        Task<IEnumerable<Supplier>> GetAllAsync();

        Task<Supplier> AddAsync(Supplier supplier);

        Task UpdateAsync(Supplier supplier);

        Task DeactivateSupplierAsync(int id, string cancelReason, int cancelledByUserId);

        // Tìm kiếm nhà cung cấp theo tên hoặc mã số thuế
        Task<IEnumerable<Supplier>> SearchSuppliersAsync(string keyword);

        // Chỉ lấy những Nhà cung cấp ĐANG HỢP TÁC (Dùng để load Dropdown khi làm Phiếu nhập kho)
        Task<IEnumerable<Supplier>> GetActiveSuppliersAsync();
    }
}