using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Pharmacy
{
    public interface ICategoryRepository : IGenericRepository<Category> 
    {
        // Lấy danh sách toàn bộ Nhóm cha ĐANG HOẠT ĐỘNG (Dùng để load cái Dropdown đầu tiên trên màn hình)
        Task<IEnumerable<Category>> GetActiveCategoriesAsync();

        // Lấy 1 Nhóm cha KÈM THEO toàn bộ các Nhóm con bên trong nó (Eager Loading)
        // Rất hữu ích khi muốn vẽ cây thư mục (Tree View) bên menu trái của phần mềm
        Task<Category> GetCategoryWithSubCategoriesAsync(Guid Id);
    }
}