using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Pharmacy;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Pharmacy 
{
    public class CategoryRepository : GenericRepository<Category>,ICategoryRepository
    {


        public CategoryRepository(ApplicationDbContext context) : base(context) { }
        public async Task<Category?> GetCategoryWithSubCategoriesAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Include(c => c.SubCategories)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted == true);
        }
    }
}