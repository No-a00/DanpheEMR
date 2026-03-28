using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;

namespace DanpheEMR.Core.Domain.EMR
{
    public class MedicationAdministration : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime AdministeredTime { get; set; }
        public string DosageGiven { get; set; }

        public string Remarks { get; set; }
        //hủy và lí do
        public string VoidReason { get; set; }
        public bool IsActive { get; set; }
        public Guid VoidedByUserId { get; set; }
        //

        public Guid AdmissionId { get; set; }
        public Admission Admission { get; set; }
        public Guid PrescriptionItemId { get; set; }
        public PrescriptionItem PrescriptionItem { get; set; }
        public Guid NurseId { get; set; }
        public Employee Nurse { get; set; }

    }
}
