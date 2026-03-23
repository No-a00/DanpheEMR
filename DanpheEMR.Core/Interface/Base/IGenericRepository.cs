namespace DanpheEMR.Core.Interface.Base
{
    public interface IGenericRepository<T> where T : class
    {
    
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid Id);

        Task AddAsync(T entity);

        void Update(T entity);


        Task DeleteAsync(Guid Id);
    }
}