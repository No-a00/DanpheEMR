using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Domain.Patients;
using System.ComponentModel.DataAnnotations;
namespace DanpheEMR.Core.Domain.Appointments
{
    public class Appointment : BaseEntity,IHasActiveStatus
    {
        public Guid Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public VisitStatus Status { get; set; } // e.g., Scheduled, Completed, Canceled
        public Guid PatientId { get; set; }
        // Hủy lịch hẹn thay vì xóa
        public bool IsActive { get; set; }
        public string Reason { get; set; }
        public Guid ReasonUserId { get; set; }
        [Required]
        public string CancelReason  { get; set; }

        public Guid ProviderId { get; set; }
        public Guid DepartmentId { get; set; }
        public Patient Patient { get; set; }
        public Employee Provider { get; set; }
        public Department Department { get; set; }


    }
}
