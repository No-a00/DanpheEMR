using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Domain.Patients;
using System.ComponentModel.DataAnnotations;
namespace DanpheEMR.Core.Domain.Appointments
{
    public class Appointment : BaseEntity,ISoftDelete
    {
        public Guid Id { get; set; }
        [Required]
        public string AppointmentCode { get; set; }
        
        [Required]
        public string PatientCode { get; set; }

        [Required]
        public string DoctorCode { get; set; }


        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public VisitStatus Status { get; set; }
        public Guid PatientId { get; set; }
        public bool IsActive { get; set; } // trạng thái hoạt đông của Appointment
        public string CancelReason { get; set; }
        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }
       public string? DeletedBy { get; set; }



        public Guid ProviderId { get; set; }
        public Guid DepartmentId { get; set; }
        public Patient Patient { get; set; }
        public Employee Provider { get; set; }
        public Department Department { get; set; }


    }
}
