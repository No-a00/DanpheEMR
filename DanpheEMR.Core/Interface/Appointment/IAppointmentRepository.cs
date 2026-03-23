using DanpheEMR.Core.Domain.Appointments;

namespace DanpheEMR.Core.Interface.Appointments
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetByIdAsync(Guid Id);
        Task<Appointment> AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);

        // Hủy lịch hẹn thay vì xóa
        Task CancelAppointmentAsync(Guid Id, string cancelReason);

        // Lọc lịch khám có phân trang hoặc điều kiện
        Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(Guid patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorAsync(Guid doctorId, DateTime date);
    }
}