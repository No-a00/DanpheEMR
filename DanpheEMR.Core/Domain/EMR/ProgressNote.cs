using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.ADTModels;
using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Appointment
{
    public class ProgressNote : BaseEntity
    {
        public string Title { get; set; }
        public DateTime NoteDate { get; set; }
        public string Subjective { get; set; }
        public string Objective { get; set; }
        public string Assessment { get; set; }
        public string Plan { get; set; }
        public int AdmissionId { get; set; }
        public Admission Admission { get; set; }
        public int ProviderId { get; set; } 
        public Employee Provider { get; set; }
    }
}
