using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface.Admin;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Admin
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly ApplicationDbContext _context;

        public AuditLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AuditLog auditLog)
        {
            await _context.Set<AuditLog>().AddAsync(auditLog);
            // Lưu ý: Việc gọi SaveChangesAsync() sẽ do UnitOfWork đảm nhiệm
        }

        public async Task<IEnumerable<AuditLog>> SearchLogsAsync(AuditLogFilter filter, int pageNumber, int pageSize)
        {
            // 1. Bắt đầu với IQueryable để xây dựng câu truy vấn động (chưa chạy DB ngay)
            IQueryable<AuditLog> query = _context.Set<AuditLog>().AsNoTracking();

            // 2. Lọc dữ liệu linh hoạt (Chỉ thêm điều kiện WHERE nếu Filter có giá trị)
            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.TableName))
                    query = query.Where(x => x.TableName == filter.TableName);

                if (filter.UserId.HasValue)
                    query = query.Where(x => x.UserId == filter.UserId.Value);

                if (!string.IsNullOrWhiteSpace(filter.Action))
                    query = query.Where(x => x.Action == filter.Action);

                // Giả định bảng AuditLog của bạn có cột CreatedAt hoặc Timestamp
                if (filter.FromDate.HasValue)
                    query = query.Where(x => x.CreatedAt >= filter.FromDate.Value);

                if (filter.ToDate.HasValue)
                    query = query.Where(x => x.CreatedAt <= filter.ToDate.Value);
            }

            // 3. Sắp xếp: Luôn ưu tiên log mới nhất lên đầu
            query = query.OrderByDescending(x => x.CreatedAt);

            // 4. Phân trang & Thực thi (Lúc này EF Core mới thực sự gửi lệnh xuống SQL Server)
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetHistoryOfRecordAsync(string tableName, string recordId)
        {
            return await _context.Set<AuditLog>()
                .AsNoTracking() // AsNoTracking giúp truy vấn Read-Only chạy nhanh hơn rất nhiều
                .Where(x => x.TableName == tableName && x.RecordId == recordId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}