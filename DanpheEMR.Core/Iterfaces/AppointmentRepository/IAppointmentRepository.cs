using DanpheEMR.Core.Iterface.Base;
using DanpheEMR.Core.Domain.Appointment;

namespace DanpheEMR.Core.Iterface.AppointmentRepository
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        // Lấy danh sách cuộc hẹn của một Bác sĩ trong ngày
        Task<IEnumerable<Appointment>> GetByProviderIdAsync(int providerId, DateTime date);

        // Kiểm tra xem bệnh nhân đã có lịch hẹn trùng giờ chưa
        Task<bool> IsSlotAvailableAsync(int providerId, DateTime date, TimeSpan time);

    }
}
