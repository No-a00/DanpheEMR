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
        }

        public async Task<IEnumerable<AuditLog>> SearchLogsAsync(AuditLogFilter filter, int pageNumber, int pageSize)
        {
          
            IQueryable<AuditLog> query = _context.Set<AuditLog>().AsNoTracking();
            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.TableName))
                    query = query.Where(x => x.TableName == filter.TableName);

                if (filter.UserId.HasValue)
                    query = query.Where(x => x.UserId == filter.UserId.Value);

                if (!string.IsNullOrWhiteSpace(filter.Action))
                    query = query.Where(x => x.Action == filter.Action);
                if (filter.FromDate.HasValue)
                    query = query.Where(x => x.CreatedAt >= filter.FromDate.Value);

                if (filter.ToDate.HasValue)
                    query = query.Where(x => x.CreatedAt <= filter.ToDate.Value);
            }
            query = query.OrderByDescending(x => x.CreatedAt);
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetHistoryOfRecordAsync(string tableName, string recordId)
        {
            return await _context.Set<AuditLog>()
                .AsNoTracking() 
                .Where(x => x.TableName == tableName && x.RecordId == recordId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}