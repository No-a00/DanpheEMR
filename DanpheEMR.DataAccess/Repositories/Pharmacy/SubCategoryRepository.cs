using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Pharmacy;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Pharmacy
{
    public class SubCategoryRepository : GenericRepository<SubCategory>,ISubCategoryRepository
    {
        public SubCategoryRepository(ApplicationDbContext context) : base(context) { }
        public async Task<IEnumerable<SubCategory>> GetActiveSubCategoriesAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(s => !s.IsDeleted)
                .OrderBy(s => s.SubCategoryName)
                .ToListAsync();
        }
        public async Task<IEnumerable<SubCategory>> GetSubCategoriesByCategoryIdAsync(Guid categoryId)
        {
            return await _dbSet.AsNoTracking()
                .Where(s => s.CategoryId == categoryId && !s.IsDeleted)
                .OrderBy(s => s.SubCategoryName)
                .ToListAsync();
        }
    }
}