
using DanpheEMR.Core.Interface.Appointments;
using DanpheEMR.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using DanpheEMR.Core.Domain.Appointments;
using DanpheEMR.Core.Enums;

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
        public async Task<Appointment?> GetByIdAsync(Guid id)
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
        public async Task CancelAppointmentAsync(Guid id, string cancelReason)
        {
            var appointment = await _dbSet.FindAsync(id);
            if (appointment != null||appointment.IsActive==false)
            {
                appointment.IsActive = false;
                appointment.CancelReason = cancelReason;
      
            }
        }

        // Lọc lịch khám có phân trang hoặc điều kiện
        public async Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date)
        {
            return await _dbSet.Where(a => a.AppointmentDate.Date == date.Date && !a.IsActive)
                .Include(a => a.Patient)
                .Include(a => a.Provider)
                .Include(a => a.Department)
                .ToListAsync();
        }
        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(Guid patientId)
        {
            return await _dbSet.Where(a => a.PatientId == patientId && !a.IsActive)
                .Include(a => a.Patient)
                .Include(a => a.Provider)
                .Include(a => a.Department)
                .ToListAsync();
        }
        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorAsync(Guid doctorId, DateTime date)
        {
            return await _dbSet.Where(a => a.ProviderId == doctorId && a.AppointmentDate.Date == date.Date && !a.IsActive)
                .Include(a => a.Patient)
                .Include(a => a.Provider)
                .Include(a => a.Department)
                .ToListAsync();
        }

        public async Task<bool> IsDoctorBusy(Guid doctorId, DateTime appointmentDate)
        {
            // Kiểm tra xem có cuộc hẹn nào của bác sĩ này 
            // trùng khít vào đúng ngày và giờ đó không
            return await _dbSet.AnyAsync(a =>
                a.ProviderId == doctorId &&
                a.AppointmentDate == appointmentDate &&
                a.Status != VisitStatus.Cancelled); // Chỉ tính những lịch chưa bị hủy
        }
    }
}
