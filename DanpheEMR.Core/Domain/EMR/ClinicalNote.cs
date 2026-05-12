using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;
using System.ComponentModel.DataAnnotations;


namespace DanpheEMR.Core.Domain.EMR
{
    public class ClinicalNote : BaseEntity,ISoftDelete
    {
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        public DateTime NoteDate { get; set; }
        public string ChiefComplaint { get; set; } // Triệu chứng chính
        public string HistoryOfPresentIllness { get; set; } // Lịch sử bệnh hiện tại
        public string ExaminationNotes { get; set; } // Ghi chú khám bệnh
         // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }
       public string? DeletedBy { get; set; }

        public Guid VisitId { get; set; } 
        public Guid PatientId { get; set; } 
        public Guid ProviderId { get; set; } 
        public Visit Visit { get; set; }
        public Patient Patient { get; set; }
        public Employee Provider { get; set; }  
    }
}
