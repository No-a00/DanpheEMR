using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;


namespace DanpheEMR.Core.Domain.Patients
{

    public class Admission : BaseEntity, IHasActiveStatus
    {
        public Guid Id { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string AdmissionNotes { get; set; }
        public AdmissionStatus Status { get; set; }

       
        public bool IsActive { get; set; } = true;
        public string CancelReason { get; set; }
        public Guid? CancelledByUserId { get; set; }

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