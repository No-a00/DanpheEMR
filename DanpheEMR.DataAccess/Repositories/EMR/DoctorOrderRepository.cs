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
            if (result == null || result.IsActive == false) return;
            result.IsActive = false;
            result.CancelReason = cancelReason;
            result.CancelledByUserId = cancelledByUserId;
        }

        public async Task<IEnumerable<DoctorOrder>> GetOrdersByVisitIdAsync(Guid visitId)
        {
            return await _dbSet.AsNoTracking()
                .Include(x => x.Visit)
                .Where(d => d.VisitId == visitId && d.IsActive == true) 
                .ToListAsync();
        }
      public async Task<IEnumerable<DoctorOrder>> GetOrdersByProviderAsync(Guid providerId, DateTime fromDate, DateTime toDate)
        {
            var endOfDay = toDate.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()
                .Where(d => d.ProviderId == providerId
                         && d.CreatedAt >= fromDate
                         && d.CreatedAt <= endOfDay
                         && d.IsActive == true)
                .ToListAsync();
        }
        public async Task<IEnumerable<DoctorOrder>> GetOrdersByStatusAsync(string status)
        {
            return await _dbSet.AsNoTracking()
                .Where(d => d.Status == status && d.IsActive == true)
                .ToListAsync();
        }
        public async Task<IEnumerable<DoctorOrder>> GetPendingOrdersAsync()
        {
            return await _dbSet.AsNoTracking()
                .Include(o => o.Provider)           
                .Include(o => o.Visit)             
                    .ThenInclude(v => v.Patient)   
                .Where(o => o.Status == "Pending" && o.IsActive)
                .ToListAsync();
        }
        public async Task UpdateOrderStatusAsync(Guid orderId, string newStatus)
        {
            var order = await _dbSet.FindAsync(orderId);
            if (order != null && order.IsActive == true)
            {
                order.Status = newStatus;
                
            }
        }
    }
}