using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Patients;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Patients
{
    public class TransferRepository : ITransferRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Transfer> _dbSet;

        public TransferRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Transfer>();
        }

        public async Task<Transfer?> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Transfer> AddAsync(Transfer transfer)
        {
            await _dbSet.AddAsync(transfer);
            return transfer;
        }

        public Task UpdateAsync(Transfer transfer)
        {
            _dbSet.Update(transfer);
            return Task.CompletedTask;
        }

        public async Task CancelTransferAsync(Guid id, string cancelReason, Guid voidedByUserId)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null || result.IsActive == false) return;

            result.IsActive = false;
            result.cancelReason = cancelReason;
            result.VoidedByUserId = voidedByUserId;
        }
        public async Task<IEnumerable<Transfer>> GetTransfersByAdmissionIdAsync(Guid admissionId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.AdmissionId == admissionId && p.IsActive == true)
                .OrderByDescending(x => x.TransferDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<Transfer>> GetPendingTransfersToDepartmentAsync(Guid toDeptId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.ToDeptId == toDeptId && p.IsActive == true && p.TransferStatus == TransferStatus.Pending)
                .OrderBy(x => x.TransferDate) 
                .ToListAsync();
        }
    }
}