using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.EMR;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.EMR
{
    public class DoctorOrderRepository : IDoctorOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<DoctorOrder> _dbSet;

        public DoctorOrderRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<DoctorOrder>();
        }

        public async Task<DoctorOrder?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<DoctorOrder> AddAsync(DoctorOrder order)
        {
            await _dbSet.AddAsync(order);
            return order;
        }

        public Task UpdateAsync(DoctorOrder order)
        {
            _dbSet.Update(order);
            return Task.CompletedTask;
        }
        public async Task CancelOrderAsync(Guid orderId, string cancelReason, Guid cancelledByUserId)
        {
          
            var result = await _dbSet.FindAsync(orderId);
            if (result == null || result.isActive == false) return;
            result.isActive = false;
            result.cancelReason = cancelReason;
            result.cancelledByUserId = cancelledByUserId;
        }

        public async Task<IEnumerable<DoctorOrder>> GetOrdersByVisitIdAsync(Guid visitId)
        {
            return await _dbSet.AsNoTracking()
                .Include(x => x.Visit)
                .Where(d => d.VisitId == visitId && d.isActive == true) 
                .ToListAsync();
        }
      public async Task<IEnumerable<DoctorOrder>> GetOrdersByProviderAsync(Guid providerId, DateTime fromDate, DateTime toDate)
        {
            var endOfDay = toDate.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()
                .Where(d => d.ProviderId == providerId
                         && d.CreatedAt >= fromDate
                         && d.CreatedAt <= endOfDay
                         && d.isActive == true)
                .ToListAsync();
        }
        public async Task<IEnumerable<DoctorOrder>> GetOrdersByStatusAsync(string status)
        {
            return await _dbSet.AsNoTracking()
                .Where(d => d.Status == status && d.isActive == true)
                .ToListAsync();
        }

        public async Task UpdateOrderStatusAsync(Guid orderId, string newStatus)
        {
            var order = await _dbSet.FindAsync(orderId);
            if (order != null && order.isActive == true)
            {
                order.Status = newStatus;
                
            }
        }
    }
}