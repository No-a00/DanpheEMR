

namespace DanpheEMR.Application.Abstractions.Infrastructure
{
    public interface ICacheService
    {
        Task<T?>  GetAsync <T>(
            string key ,
            CancellationToken cancellationToken = default);
        Task Set<T>(
            string key ,
            T value,
            TimeSpan expriration,
            CancellationToken cancellationToken = default);
        Task RemoveAsync<T>(
            string key,
            CancellationToken cancellation = default);
        Task RemoveByPrefiAsync<T>(
            string prefix,
            CancellationToken cancellation = default);
    }
}
