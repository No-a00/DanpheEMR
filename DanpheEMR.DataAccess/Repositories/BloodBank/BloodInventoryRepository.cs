using DanpheEMR.Core.Domain.BloodBank;
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Interface.BloodBank;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;


namespace DanpheEMR.DataAccess.Repositories.BloodBank
{
    public class BloodInventoryRepository : IBloodInventoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<BloodInventory> _dbSet;

        public BloodInventoryRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<BloodInventory>();
        }

        public async Task AddAsync(BloodInventory bloodBag)
        {
            await _dbSet.AddAsync(bloodBag);
        }

        public void Update(BloodInventory bloodBag)
        {
            _dbSet.Update(bloodBag);
        }

        public async Task<List<BloodInventory>> GetAvailableBagsAsync(Guid bloodGroupId, int count)
        {
            return await _dbSet
                
                .Where(b => b.BloodGroupId == bloodGroupId
                         
                         && b.Status == BloodBagStatus.Available
                       
                         && b.ExpiryDate >= DateTime.Now)
               
                .OrderBy(b => b.ExpiryDate)
                
                .Take(count)
                .ToListAsync();
        }
        //Lấy kho máu theo nhóm máu 
        public async Task<IEnumerable<BloodInventory>> GetAllAvailableBagsAsync(Guid? bloodGroupId)
        {
            
            var query = _dbSet
                .Include(b => b.BloodGroup) 
                .Where(b => b.Status == BloodBagStatus.Available && b.ExpiryDate >= DateTime.Now);
            if (bloodGroupId.HasValue)
            {
                query = query.Where(b => b.BloodGroupId == bloodGroupId.Value);
            }

            return await query.OrderBy(b => b.ExpiryDate).ToListAsync();
        }
        // Đếm xem trong kho còn bao nhiêu bịch máu sẵn sàng
        public async Task<int> CountAvailableStockAsync(Guid bloodGroupId)
        {
            return await _dbSet.CountAsync(b =>
                b.BloodGroupId == bloodGroupId
                && b.Status == BloodBagStatus.Available
                && b.ExpiryDate >= DateTime.Now);
        }
    }
}