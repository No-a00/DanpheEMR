using DanpheEMR.Core.Domain.Admin;

using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;

namespace DanpheEMR.Core.Domain.EMR
{
    public class ProgressNote : BaseEntity,ISoftDelete
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime NoteDate { get; set; }
        public string Subjective { get; set; }
        public string Objective { get; set; }
        public string Assessment { get; set; }
        public string Plan { get; set; }
        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string Reason { get; set; }
        public Guid? DeletedBy { get; set; }


        public Guid AdmissionId { get; set; }
        public Guid ProviderId { get; set; } 
        public Admission Admission { get; set; }
       
        public Employee Provider { get; set; }
    }
}
