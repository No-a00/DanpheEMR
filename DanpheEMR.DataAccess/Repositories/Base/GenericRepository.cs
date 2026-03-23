using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Interface.Base;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            // Trả về List<T> đúng như định nghĩa trong Interface của bạn
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            // FindAsync là cách tối ưu nhất để tìm theo Khóa chính (Primary Key)
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity != null)
            {
                // Kiểm tra xem thực thể này có triển khai ISoftDelete không
                if (entity is ISoftDelete softDeleteEntity)
                {
                    // NẾU CÓ: Cập nhật trạng thái thành Xóa mềm
                    softDeleteEntity.IsDeleted = true;
                    _dbSet.Update(entity);
                } 
                else
                {
                    // NẾU KHÔNG: Thực hiện Xóa cứng (xóa hẳn khỏi Database) như bình thường
                    _dbSet.Remove(entity);
                }
            }
        }
    }
}