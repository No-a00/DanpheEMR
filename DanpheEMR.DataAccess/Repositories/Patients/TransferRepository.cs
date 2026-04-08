using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Patients;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using DanpheEMR.DataAccess.Repositories.Base;

namespace DanpheEMR.DataAccess.Repositories.Patients
{
    public class TransferRepository : GenericRepository<Transfer>,ITransferRepository
    {


        public TransferRepository(ApplicationDbContext context) : base(context) { }

        
        public async Task<IEnumerable<Transfer>> GetTransfersByAdmissionIdAsync(Guid admissionId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.AdmissionId == admissionId && !p.IsDeleted )
                .OrderByDescending(x => x.TransferDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<Transfer>> GetPendingTransfersToDepartmentAsync(Guid toDeptId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.ToDeptId == toDeptId && !p.IsDeleted && p.TransferStatus == TransferStatus.Pending)
                .OrderBy(x => x.TransferDate) 
                .ToListAsync();
        }
    }
}