
using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Pharmacy
{
    public interface IItemRepository : IGenericRepository<Item>
    {

        // Lọc danh sách theo Nhóm thuốc 
        Task<IEnumerable<Item>> GetItemsBySubCategoryAsync(Guid subCategoryId);
        // Tìm kiếm linh hoạt để Dược sĩ gõ tên tìm thuốc nhanh
        Task<IEnumerable<Item>> SearchItemsAsync(string keyword);

        // QUAN TRỌNG: Lọc ra các loại thuốc sắp hết (Tồn kho < ReorderLevel) để đi mua thêm
        Task<IEnumerable<Item>> GetItemsNearingReorderLevelAsync();
    }
}