using DanpheEMR.Core.Interface;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace DanpheEMR.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _currentTransaction;

        // Tiêm ApplicationDbContext vào đây
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Bản thân DbContext đã là một UoW, nó sẽ gộp mọi thay đổi
            // từ các Repository khác nhau lại và lưu 1 lần duy nhất.
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return null;
            }
            _currentTransaction = await _context.Database.BeginTransactionAsync();
            return _currentTransaction;
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync(); // Lưu thay đổi trước
                if (_currentTransaction != null)
                {
                    await _currentTransaction.CommitAsync(); // Xác nhận giao dịch với SQL Server
                }
            }
            catch
            {
                await RollbackTransactionAsync(); // Nếu có lỗi ở bất kỳ bảng nào, hoàn tác tất cả!
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.RollbackAsync(); // Hủy bỏ mọi thay đổi chưa commit
                }
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}