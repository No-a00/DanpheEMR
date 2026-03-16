using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanpheEMR.Core.Domain.Pharmacy
{
    public interface ISubCategoryRepository
    {
        Task<SubCategory> GetByIdAsync(int id);
        Task<SubCategory> AddAsync(SubCategory subCategory);
        Task UpdateAsync(SubCategory subCategory);
        Task<IEnumerable<SubCategory>> GetActiveSubCategoriesAsync();

        // Lấy danh sách các Nhóm con thuộc về một Nhóm cha cụ thể 
        Task<IEnumerable<SubCategory>> GetSubCategoriesByCategoryIdAsync(int categoryId);
    }
}