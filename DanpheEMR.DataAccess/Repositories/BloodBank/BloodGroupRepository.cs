
using DanpheEMR.Core.Domain.BloodBank;
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Interface.BloodBank;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.BloodBank
{
    public  class BloodGroupRepository : IBloodGroupRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<BloodGroup> _dbSet;
        public BloodGroupRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<BloodGroup>();
        }
        public async Task<BloodGroup?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(b=>b.Id==id);
        }
        public async Task<BloodGroup?> GetByNameAsync(BloodType bloodGroupName)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(b => b.BloodGroupName == bloodGroupName);
        }
        //lấy thông tin nhóm máu cùng với danh sách người hiến máu        
        public async Task<BloodGroup?> GetWithDonorsAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                               .Include(x => x.BloodDonors)
                               .FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<BloodGroup> AddAsync(BloodGroup bloodGroup)
        {
            await _dbSet.AddAsync(bloodGroup);
            return bloodGroup;
        } //(cập nhật lần đầu và có thể không cần thay đổi)
    }
}
