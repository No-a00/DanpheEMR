
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DanpheEMR.DataAccess.Repositories.Admin
{
    public  class SystemParameterRepository : ISystemParameterRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<SystemParameter> _dbSet;
        private readonly IMemoryCache _cache;
        public SystemParameterRepository(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _dbSet = _context.Set<SystemParameter>();
            _cache = cache;
        }
        public async Task<IEnumerable<SystemParameter>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<SystemParameter> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(SystemParameter parameter)
        {
            _dbSet.Update(parameter);

            // Xóa dữ liệu cũ trong RAM đi. 
            // Lần truy cập tiếp theo, hệ thống sẽ tự động xuống DB lấy bản mới nhất (10%) và lưu lại vào RAM.
            string cacheKey = $"SysParam_{parameter.ParameterName}";
            _cache.Remove(cacheKey);
        }
        public async Task<SystemParameter?> GetByNameAsync(string parameterName)
        {
            // Tạo một "chìa khóa" (Key) duy nhất cho từng tham số trong RAM
            string cacheKey = $"SysParam_{parameterName}";

            // Kiểm tra xem trong RAM (Cache) đã có dữ liệu này chưa?
            if (!_cache.TryGetValue(cacheKey, out SystemParameter? parameter))
            {
                // Nếu RAM chưa có (hoặc đã bị xóa), mới chọc xuống Database lấy
                parameter = await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.ParameterName == parameterName);

                // Nếu Database có dữ liệu, cất một bản copy vào RAM
                if (parameter != null)
                {
                    // Cấu hình thời gian sống của Cache (VD: Lưu trong 24 tiếng)
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromHours(24));

                    _cache.Set(cacheKey, parameter, cacheEntryOptions);
                }
            }

            // Trả về dữ liệu (Lấy từ RAM cực nhanh, hoặc vừa mới lấy từ DB lên)
            return parameter;
        }
    }
}



