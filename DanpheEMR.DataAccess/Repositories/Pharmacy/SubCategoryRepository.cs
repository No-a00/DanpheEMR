using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Pharmacy;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Pharmacy
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<SubCategory> _dbSet;

        public SubCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<SubCategory>();
        }

        public async Task<SubCategory?> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<SubCategory> AddAsync(SubCategory subCategory)
        {
            await _dbSet.AddAsync(subCategory);
            return subCategory;
        }

        public Task UpdateAsync(SubCategory subCategory)
        {
            _dbSet.Update(subCategory);
            return Task.CompletedTask;
        }
        public async Task DeactivateSubCategoryAsync(int id, string cancelReason, int cancelledByUserId)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.CancelReason = cancelReason;
            result.CancelledByUserId = cancelledByUserId;
        }

        public async Task<IEnumerable<SubCategory>> GetActiveSubCategoriesAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(s => s.IsActive == true)
                .OrderBy(s => s.SubCategoryName)
                .ToListAsync();
        }
        public async Task<IEnumerable<SubCategory>> GetSubCategoriesByCategoryIdAsync(int categoryId)
        {
            return await _dbSet.AsNoTracking()
                .Where(s => s.CategoryId == categoryId && s.IsActive == true)
                .OrderBy(s => s.SubCategoryName)
                .ToListAsync();
        }
    }
}