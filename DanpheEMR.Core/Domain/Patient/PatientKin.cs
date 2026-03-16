using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.ADTModels
{
    public class PatientKin : BaseEntity
    {
        public int Id { get; set; }
        public string KinName { get; set; }
        public string RelationShip { get; set; }
        public string ContactNumber { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
