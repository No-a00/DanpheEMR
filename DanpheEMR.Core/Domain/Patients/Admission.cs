using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Enums;
using System.ComponentModel.DataAnnotations;


namespace DanpheEMR.Core.Domain.Patients
{

    public class Admission : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string AdmissionNotes { get; set; }
        public AdmissionStatus Status { get; set; }


        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }
       public string? DeletedBy { get; set; }

        public Guid PatientId { get; set; }
        public Guid VisitId { get; set; }
        public Guid AdmittingDoctorId { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Visit Visit { get; set; }
        public virtual Employee AdmittingDoctor { get; set; }

        public virtual Discharge Discharge { get; set; } 
        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}