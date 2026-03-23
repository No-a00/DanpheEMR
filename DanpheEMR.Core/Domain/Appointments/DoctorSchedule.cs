using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;

public class DoctorSchedule : BaseEntity
{
    public Guid Id { get; set; }
    public DateTime DayOfWeek { get; set; }

    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int MaxPatients { get; set; }
    public Guid ProviderId { get; set; }
    public Guid DepartmentId { get; set; }

    public Employee Provider { get; set; }
    public Department Department { get; set; }
}