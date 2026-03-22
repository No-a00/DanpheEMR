
using DanpheEMR.Core.Interface;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore.Storage; 

namespace DanpheEMR.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        // Biến này để UnitOfWork tự nhớ giao dịch hiện tại đang làm dở
        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();

                if (_currentTransaction != null)
                {
                    await _currentTransaction.CommitAsync();
                }
            }
            catch
            {
                // Nếu Commit lỗi (ví dụ đứt mạng, lỗi khóa dữ liệu...), tự động Rollback
                await RollbackTransactionAsync();
                throw; // Ném lỗi ra ngoài để tầng Service biết mà xử lý
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
                    await _currentTransaction.RollbackAsync();
                }
            }
            finally
            {
                // Xóa transaction sau khi hủy
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        // Hàm dọn dẹp bộ nhớ (bắt buộc vì interface kế thừa IDisposable)
        public void Dispose()
        {
            _context.Dispose();
            _currentTransaction?.Dispose();
        }
    }
}