using DanpheEMR.Core.Domain.Pharmacy;
using DanpheEMR.Core.Interface.Pharmacy;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Pharmacy 
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Category> _dbSet;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Category>();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> AddAsync(Category category)
        {
            await _dbSet.AddAsync(category);
            return category;
        }

        public Task UpdateAsync(Category category)
        {
            _dbSet.Update(category);
            return Task.CompletedTask;
        }
        public async Task CancelCategoryAsync(Guid id, string cancelReason, Guid userIdCancel)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.CancelReason = cancelReason;
            result.UserIdCancel = userIdCancel;
        }
        public async Task<IEnumerable<Category>> GetActiveCategoriesAsync()
        {
            return await _dbSet.AsNoTracking()
                // Gợi ý: Nếu bảng Category của bạn có cột ParentId để phân cấp Cha-Con, 
                // bạn có thể thêm "&& c.ParentId == null" để chỉ load các nhóm cấp 1 lên Dropdown
                .Where(c => c.IsActive == true)
                .ToListAsync();
        }

        // 2. HOÀN THIỆN: Lấy Nhóm cha KÈM THEO các Nhóm con bên trong nó (Eager Loading)
        public async Task<Category?> GetCategoryWithSubCategoriesAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                // Giả định Navigation Property chứa danh sách nhóm con của bạn tên là SubCategories (hoặc Children)
                .Include(c => c.SubCategories)
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive == true);
        }
    }
}