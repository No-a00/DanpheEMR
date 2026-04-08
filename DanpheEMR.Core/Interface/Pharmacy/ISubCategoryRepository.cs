using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Pharmacy
{
    public interface ISubCategoryRepository : IGenericRepository <SubCategory>
    {
        Task<IEnumerable<SubCategory>> GetActiveSubCategoriesAsync();

        Task<IEnumerable<SubCategory>> GetSubCategoriesByCategoryIdAsync(Guid categoryId);
    }
}