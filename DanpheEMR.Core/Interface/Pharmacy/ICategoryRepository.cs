using DanpheEMR.Core.Domain.Pharmacy;

namespace DanpheEMR.Core.Interface.Pharmacy
{
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(int id);
        Task<Category> AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task CancelCategoryAsync(int id, string cancelReason, int userIdCancel);
        // Lấy danh sách toàn bộ Nhóm cha ĐANG HOẠT ĐỘNG (Dùng để load cái Dropdown đầu tiên trên màn hình)
        Task<IEnumerable<Category>> GetActiveCategoriesAsync();

        // Lấy 1 Nhóm cha KÈM THEO toàn bộ các Nhóm con bên trong nó (Eager Loading)
        // Rất hữu ích khi muốn vẽ cây thư mục (Tree View) bên menu trái của phần mềm
        Task<Category> GetCategoryWithSubCategoriesAsync(int id);
    }
}