using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.ADTModels
{
    public class Discharge : BaseEntity
    {
        public int Id { get; set; }
        public DateTime DischargeDate { get; set; }
        public string DischargeCondition { get; set; }
        public string DischargeNotes { get; set; }
        public int PatientId { get; set; }
        public int AddmissionId { get; set; }
        public Patient Patient { get; set; }
        public Admission Addmission { get; set; }
    }
}
