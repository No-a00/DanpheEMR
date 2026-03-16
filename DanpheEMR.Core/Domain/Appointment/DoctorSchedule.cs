using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Appointment
{
    public class DoctorSchedule : BaseEntity
    {
        public int Id { get; set; }
        public int DayOfWeek { get; set; } // 0 = Sunday, 1 = Monday, ..., 6 = Saturday
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int MaxPatients { get; set; } // Số lượng bệnh nhân tối đa trong khung giờ này
        public int ProviderId { get; set; } // Id của bác sĩ
        public int  DepartmentId {  get; set; }
        public Employee Provider { get; set; } // Thông tin bác sĩ
        public Department Department { get; set; } // Thông tin khoa
    }
}
