
using DanpheEMR.Core.Interface.Appointments;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using DanpheEMR.Core.Domain.Appointments;
using DanpheEMR.Core.Enums;
using DanpheEMR.DataAccess.Repositories.Base;

namespace DanpheEMR.DataAccess.Repositories.Appointments
{
    public class AppointmentRepository : GenericRepository<Appointment>,IAppointmentRepository
    {
       
        public AppointmentRepository(ApplicationDbContext context) : base (context) {  }
        // Lọc lịch khám có phân trang hoặc điều kiện
        public async Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date)
        {
            return await _dbSet.Where(a => a.AppointmentDate.Date == date.Date && !a.IsDeleted)
                .Include(a => a.Patient)
                .Include(a => a.Provider)
                .Include(a => a.Department)
                .ToListAsync();
        }
        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(Guid patientId)
        {
            return await _dbSet.Where(a => a.PatientId == patientId && !a.IsDeleted)
                .Include(a => a.Patient)
                .Include(a => a.Provider)
                .Include(a => a.Department)
                .ToListAsync();
        }
        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorAsync(Guid doctorId, DateTime date)
        {
            return await _dbSet.Where(a => a.ProviderId == doctorId && a.AppointmentDate.Date == date.Date && !a.IsDeleted)
                .Include(a => a.Patient)
                .Include(a => a.Provider)
                .Include(a => a.Department)
                .ToListAsync();
        }
        public Task<Appointment?> GetByCodeAsync(string appointmentCode) 
        {
            return _dbSet.FirstOrDefaultAsync(a => a.AppointmentCode == appointmentCode && !a.IsDeleted);
        }
        public async Task<bool> IsDoctorBusy(string DoctorCode, DateTime appointmentDate)
        {
            return await _dbSet.AnyAsync(a =>
                a.DoctorCode == DoctorCode &&
                a.AppointmentDate == appointmentDate &&
                a.Status != VisitStatus.Cancelled); 
        }
    }
}
