using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Nums;

namespace DanpheEMR.Core.Domain.Patients
{
    public class Admission : BaseEntity
    {
        public int Id { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string AdmissionNotes { get; set; }
        public VisitStatus Status { get; set; } // e.g., "Admitted", "Discharged", "Transferred"
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
