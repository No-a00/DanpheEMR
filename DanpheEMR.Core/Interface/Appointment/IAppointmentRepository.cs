using DanpheEMR.Core.Domain.Appointments;

namespace DanpheEMR.Core.Interface.Appointments
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetByIdAsync(int id);
        Task<Appointment> AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);

        // Hủy lịch hẹn thay vì xóa
        Task CancelAppointmentAsync(int id, string cancelReason);

        // Lọc lịch khám có phân trang hoặc điều kiện
        Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(int patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorAsync(int doctorId, DateTime date);
    }
}