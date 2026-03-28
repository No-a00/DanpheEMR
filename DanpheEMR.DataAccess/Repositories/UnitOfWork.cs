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
        private readonly ICurrentUserService _currentUserService;

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

            var auditLogs = new List<AuditLog>();

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is AuditLog) continue;

                var auditLog = new AuditLog
                {
                    Id = Guid.NewGuid(),
                    TableName = entry.Entity.GetType().Name,
                    Action = entry.State.ToString(),
                    Timestamp = DateTime.UtcNow, 
                    UserId = _currentUserService.UserId
                };

                auditLogs.Add(auditLog);
            }

            if (auditLogs.Any())
            {
                await _dbContext.Set<AuditLog>().AddRangeAsync(auditLogs, cancellationToken);
            }

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync(cancellationToken: default);
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContext.Database.CommitTransactionAsync(cancellationToken: default);
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync(cancellationToken: default);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}