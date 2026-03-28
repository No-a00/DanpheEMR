using DanpheEMR.Core.Domain.Patients;
using DanpheEMR.Core.Interface.Patients;
using DanpheEMR.DataAccess.Data;
using DanpheEMR.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DanpheEMR.DataAccess.Repositories.Patients
{
    public class VisitRepository : GenericRepository<Visit>, IVisitRepository
    {
        public VisitRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Visit>> GetActiveVisitsByProviderAsync(Guid providerId, DateTime date)
        {
            return await _context.Set<Visit>()
                .Include(v => v.Patient)
                .Where(v => v.ProviderId == providerId && v.VisitDate.Date == date.Date && v.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Visit>> GetVisitsByPatientIdAsync(Guid patientId)
        {
            return await _context.Set<Visit>()
                .Include(v => v.Department)
                .Include(v => v.Provider)
                .Where(v => v.PatientId == patientId)
                .OrderByDescending(v => v.VisitDate)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Visit?> GetVisitWithAllDetailsAsync(Guid id)
        {
            return await _context.Set<Visit>()
                .Include(v => v.Patient)
                .Include(v => v.Department)
                .Include(v => v.Provider)
                .Include(v => v.ClinicalNotes)
                .Include(v => v.Diagnoses)
                .Include(v => v.Prescriptions)
                .Include(v => v.Vitals)
                .Include(v => v.DoctorOrders)
                .Include(v => v.Admissions)
                .AsSplitQuery() 
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
        }
        public async Task<string> GenerateVisitCodeAsync()
        {
            string prefix = $"VIS-{DateTime.Now:yyMM}-"; 

            var count = await _context.Set<Visit>().CountAsync(v => v.VisitCode.StartsWith(prefix));

         
            return $"{prefix}{(count + 1).ToString("D3")}";
        }


        public async Task<int> GenerateQueueNoAsync(Guid departmentId, DateTime date)
        {
            var maxQueue = await _context.Set<Visit>()
                .Where(v => v.DepartmentId == departmentId && v.VisitDate.Date == date.Date)
                .MaxAsync(v => (int?)v.QueueNo) ?? 0;

            return maxQueue + 1;
        }
    }
}