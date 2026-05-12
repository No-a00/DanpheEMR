using System.Linq.Expressions;

namespace DanpheEMR.Core.Interface.Base
{
    public interface IGenericRepository<T> where T : class
    {
    
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid Id);

        Task AddAsync(T entity);

        void Update(T entity);


        Task DeleteAsync(Guid id, string? deletedBy = null, string? reason = null);

        //Tìm 1 bản ghi theo bất kỳ điều kiện nào
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    }
}