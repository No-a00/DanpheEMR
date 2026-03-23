using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Patients
{
    public class PatientKin : BaseEntity
    {
        public Guid Id { get; set; }
        public string KinName { get; set; }
        public string RelationShip { get; set; }
        public string ContactNumber { get; set; }

        public Guid patientId { get; set; }
        public Patient Patient { get; set; }
    }
}
