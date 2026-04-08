using DanpheEMR.Core.Domain.Appointments;
using DanpheEMR.Core.Interface.Base;

namespace DanpheEMR.Core.Interface.Appointments
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        // Kiểm tra xem bác sĩ đã có lịch khám nào trùng vào thời điểm này chưa
        Task<bool> IsDoctorBusy(Guid doctorId, DateTime appointmentDate);
        Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(Guid patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorAsync(Guid doctorId, DateTime date);
    }
}