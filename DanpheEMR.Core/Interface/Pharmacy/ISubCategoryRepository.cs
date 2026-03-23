using DanpheEMR.Core.Domain.Pharmacy;

namespace DanpheEMR.Core.Interface.Pharmacy
{
    public interface ISubCategoryRepository
    {
        Task<SubCategory?> GetByIdAsync(Guid Id);

        Task<SubCategory> AddAsync(SubCategory subCategory);

        Task UpdateAsync(SubCategory subCategory);

        Task DeactivateSubCategoryAsync(Guid Id, string cancelReason, int cancelledByUserId);

        Task<IEnumerable<SubCategory>> GetActiveSubCategoriesAsync();

        Task<IEnumerable<SubCategory>> GetSubCategoriesByCategoryIdAsync(int categoryId);
    }
}