using DanpheEMR.Core.Domain.EMR;
using DanpheEMR.Core.Interface.EMR;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.EMR
{
    public class DoctorOrderRepository : GenericRepository<DoctorOrder>,IDoctorOrderRepository
    {
        
        public DoctorOrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        
        public async Task<IEnumerable<DoctorOrder>> GetOrdersByVisitIdAsync(Guid visitId)
        {
            return await _dbSet.AsNoTracking()
                .Include(x => x.Visit)
                .Where(d => d.VisitId == visitId && !d.IsDeleted) 
                .ToListAsync();
        }
      public async Task<IEnumerable<DoctorOrder>> GetOrdersByProviderAsync(Guid providerId, DateTime fromDate, DateTime toDate)
        {
            var endOfDay = toDate.Date.AddDays(1).AddTicks(-1);

            return await _dbSet.AsNoTracking()
                .Where(d => d.ProviderId == providerId
                         && d.CreatedAt >= fromDate
                         && d.CreatedAt <= endOfDay
                         && !d.IsDeleted)
                .ToListAsync();
        }
        public async Task<IEnumerable<DoctorOrder>> GetOrdersByStatusAsync(string status)
        {
            return await _dbSet.AsNoTracking()
                .Where(d => d.Status == status && !d.IsDeleted)
                .ToListAsync();
        }
        public async Task<IEnumerable<DoctorOrder>> GetPendingOrdersAsync()
        {
            return await _dbSet.AsNoTracking()
                .Include(o => o.Provider)           
                .Include(o => o.Visit)             
                    .ThenInclude(v => v.Patient)   
                .Where(o => o.Status == "Pending" && !o.IsDeleted)
                .ToListAsync();
        }
        public async Task UpdateOrderStatusAsync(Guid orderId, string newStatus)
        {
            var order = await _dbSet.FindAsync(orderId);
            if (order != null && !order.IsDeleted)
            {
                order.Status = newStatus;
                
            }
        }
    }
}