
using DanpheEMR.Core.Domain.Appointments;
using DanpheEMR.Core.Interfaces.Appointment;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Appointments
{
    public  class HolidayRepository : GenericRepository<Holiday>,IHolidayRepository
    {
        public HolidayRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<Holiday?> GetHolidayByProviderAndDateAsync(int providerId, DateTime date)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(h => h.ProviderId == providerId && h.Date.Date == date.Date);
        }
        public async Task<IEnumerable<Holiday>> GetHolidaysForProviderInMonthAsync(int providerId, int year, int month)
        {
            return await _dbSet.AsNoTracking().Where(h => h.ProviderId == providerId && h.Date.Year == year && h.Date.Month == month).ToListAsync();
        }
    }
}
