//using DanpheEMR.WEB.Iterface;
//using Microsoft.EntityFrameworkCore;

//namespace DanpheEMR.WEB.Repository
//{
//    public class GenericRepository<T> : IGenericRepository<T> where T : class
//    {
//        // Đổi thành protected để lớp con  có thể xài ké
//        protected readonly DbContext _context;
//        public GenericRepository(DbContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<T>> GetAllAsync()
//        {
//            return await _context.Set<T>().ToListAsync();
//        }

//        public async Task<T> GetByIdAsync(int id)
//        {
//            return await _context.Set<T>().FindAsync(id);
//        }

//        public async Task AddAsync(T entity)
//        {
//            await _context.Set<T>().AddAsync(entity);
//        }
//        public void Update(T entity)
//        {
//            _context.Set<T>().Update(entity);
//        }

      
//        public async Task DeleteAsync(int id)
//        {
//            var entity = await _context.Set<T>().FindAsync(id);
//            if (entity != null)
//            {
//                _context.Set<T>().Remove(entity);
//            }
//        }
//    }
//}