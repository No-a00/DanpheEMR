
using DanpheEMR.Core.Domain.Pharmacy;

namespace DanpheEMR.Core.Interface.Pharmacy
{
    public interface IItemRepository
    {
        Task<Item> GetByIdAsync(Guid Id);
        Task<Item> AddAsync(Item item);
        Task UpdateAsync(Item item);
        Task DeactivateItemAsync(Guid Id,string cancelReason,int cancelUserId);

        // Lọc danh sách theo Nhóm thuốc 
        Task<IEnumerable<Item>> GetItemsBySubCategoryAsync(int subCategoryId);
        // Tìm kiếm linh hoạt để Dược sĩ gõ tên tìm thuốc nhanh
        Task<IEnumerable<Item>> SearchItemsAsync(string keyword);

        // QUAN TRỌNG: Lọc ra các loại thuốc sắp hết (Tồn kho < ReorderLevel) để đi mua thêm
        Task<IEnumerable<Item>> GetItemsNearingReorderLevelAsync();
    }
}