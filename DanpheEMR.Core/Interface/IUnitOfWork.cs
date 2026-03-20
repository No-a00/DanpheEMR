using Microsoft.EntityFrameworkCore.Storage;

namespace DanpheEMR.Core.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        // 3 Hàm dưới đây dùng cho các quy trình nghiệp vụ phức tạp 
        // (VD: Vừa trừ kho thuốc, vừa tạo hóa đơn, vừa ghi log)
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

    }
}
