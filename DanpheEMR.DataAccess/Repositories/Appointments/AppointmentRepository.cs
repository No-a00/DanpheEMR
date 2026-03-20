
using DanpheEMR.Core.Interface.Appointments;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using DanpheEMR.Core.Domain.Appointments;

namespace DanpheEMR.DataAccess.Repositories.Appointments
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Appointment> _dbSet; 
        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Appointment>();
        }
        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(a => a.Patient)
                .Include(a => a.Provider)
                .Include(a => a.Department)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            var result = await _dbSet.AddAsync(appointment);
            return result.Entity;
        }
        public async Task UpdateAsync(Appointment appointment)
        {
            _dbSet.Update(appointment);
        }

        // Hủy lịch hẹn thay vì xóa
        public async Task CancelAppointmentAsync(int id, string cancelReason)
        {
            var appointment = await _dbSet.FindAsync(id);
            if (appointment != null)
            {
                appointment.IsCanceled = true;
                appointment.CancelReason = cancelReason;
                _dbSet.Update(appointment);
            }
        }

        // Lọc lịch khám có phân trang hoặc điều kiện
        public async Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date)
        {
            return await _dbSet.Where(a => a.AppointmentDate.Date == date.Date && !a.IsCanceled)
                .Include(a => a.Patient)
                .Include(a => a.Provider)
                .Include(a => a.Department)
                .ToListAsync();
        }
        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(int patientId)
        {
            return await _dbSet.Where(a => a.PatientId == patientId && !a.IsCanceled)
                .Include(a => a.Patient)
                .Include(a => a.Provider)
                .Include(a => a.Department)
                .ToListAsync();
        }
        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorAsync(int doctorId, DateTime date)
        {
            return await _dbSet.Where(a => a.ProviderId == doctorId && a.AppointmentDate.Date == date.Date && !a.IsCanceled)
                .Include(a => a.Patient)
                .Include(a => a.Provider)
                .Include(a => a.Department)
                .ToListAsync();
        }
    }
}
