using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Interface;
using DanpheEMR.Core.Interfaces.Base;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;


namespace DanpheEMR.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        // 1. KHAI BÁO BIẾN Ở ĐÂY
        private readonly ICurrentUserService _currentUserService;

        // 2. TIÊM VÀO CONSTRUCTOR Ở ĐÂY
        public UnitOfWork(
            ApplicationDbContext dbContext,
            ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntries = _dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is AuditLog) continue;

                var auditLog = new AuditLog
                {
                    Id = Guid.NewGuid(),
                    TableName = entry.Entity.GetType().Name,
                    Action = entry.State.ToString(),
                    Timestamp = DateTime.Now,

                    // Bây giờ biến này đã hoạt động hợp lệ!
                    UserId = _currentUserService.UserId
                };

                _dbContext.Set<AuditLog>().Add(auditLog);
            }

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}