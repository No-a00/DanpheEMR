using DanpheEMR.Core.Domain.ADTModels;
using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Appointment
{
    public class MedicationAdministration : BaseEntity
    {
        public int Id { get; set; }
        public DateTime AdministeredTime { get; set; }
        public string DosageGiven { get; set; }

        public string Remarks { get; set; }
        public int AdmissionId { get; set; }
        public Admission Admission { get; set; }
        public int PrescriptionItemId { get; set; }
        public PrescriptionItem PrescriptionItem { get; set; }
        public int NurdeId { get; set; }   
      
    }
}
