using DanpheEMR.Core.Domain.Admin;

using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;

namespace DanpheEMR.Core.Domain.EMR
{
    public class ProgressNote : BaseEntity,IHasActiveStatus
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime NoteDate { get; set; }
        public string Subjective { get; set; }
        public string Objective { get; set; }
        public string Assessment { get; set; }
        public string Plan { get; set; }
        //hủy và lí do
        public bool IsActive { get; set; }
        public string voidReason { get; set; }
        public Guid voidedByUserId { get; set; }
        public Guid AdmissionId { get; set; }
        public Admission Admission { get; set; }
        public Guid ProviderId { get; set; } 
        public Employee Provider { get; set; }
    }
}
