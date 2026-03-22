using Application.Common.Enums;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Patients
{
    public class Admission : BaseEntity
    {
        public int Id { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string AdmissionNotes { get; set; }
        public AdmissionStatus Status { get; set; } // e.g., "Admitted", "Discharged", "Transferred"
        //huy va li do
        public bool IsActive { get; set; }
        public string ReasonCancel { get; set; }
        public int CancelUserId{ get; set; }
        //
        public int PatientId { get; set; }
        public int VisitId { get; set; }
        public int AdmittingDoctorId { get; set; }
        public Patient Patient { get; set; }
        public Visit Visit { get; set; }
        public Employee AdmittingDoctor { get; set; }
        public Discharge Discharges { get; set; }
        public ICollection<Transfer> Transfers { get; set; }

    }
}
