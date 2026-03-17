namespace DanpheEMR.Core.Interface.Base
{
    public interface IGenericRepository<T> where T : class
    {
    
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);

        void Update(T entity);


        Task DeleteAsync(int id);
    }
}