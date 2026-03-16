using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.ADTModels;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Nums;

namespace DanpheEMR.Core.Domain.Appointment
{
    public class Appointment : BaseEntity
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public VisitStatus Status { get; set; } // e.g., Scheduled, Completed, Canceled
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public int DepartmentId { get; set; }
        public Patient Patient { get; set; }
        public Employee Provider { get; set; }
        public Department Department { get; set; }


    }
}
