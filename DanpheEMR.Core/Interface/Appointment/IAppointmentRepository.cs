using DanpheEMR.Core.Domain.Appointments;

namespace DanpheEMR.Core.Interface.Appointments
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetByIdAsync(Guid Id);
        Task<Appointment> AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);

        // --- THÊM DÒNG NÀY VÀO ĐÂY ---
        // Kiểm tra xem bác sĩ đã có lịch khám nào trùng vào thời điểm này chưa
        Task<bool> IsDoctorBusy(Guid doctorId, DateTime appointmentDate);

        Task CancelAppointmentAsync(Guid Id, string cancelReason,Guid resasonUserId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(Guid patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorAsync(Guid doctorId, DateTime date);
    }
}