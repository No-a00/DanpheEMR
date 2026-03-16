using DanpheEMR.Core.Domain.Appointment;
using DanpheEMR.Core.Iterface.Base;

namespace DanpheEMR.Core.Iterfaces.Appointment
{
    public interface IHolidayRepository : IGenericRepository<Holiday>
    {
        // 1. Lấy thông tin ngày nghỉ (nếu có) của một Bác sĩ trong một ngày cụ thể
        // Trả về null nếu ngày đó đi làm bình thường
        Task<Holiday> GetHolidayByProviderAndDateAsync(int providerId, DateTime date);

        // 2. Lấy danh sách các ngày nghỉ trong một tháng (Dùng để hiển thị lên lịch/Calendar UI)
        Task<IEnumerable<Holiday>> GetHolidaysForProviderInMonthAsync(int providerId, int year, int month);
    }
}
