using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;


namespace DanpheEMR.Core.Domain.EMR
{
    public class ClinicalNote : BaseEntity
    {
        public int Id { get; set; }
        public DateTime NoteDate { get; set; }
        public string ChiefComplaint { get; set; } // Triệu chứng chính
        public string HistoryOfPresentIllness { get; set; } // Lịch sử bệnh hiện tại
        public string ExaminationNotes { get; set; } // Ghi chú khám bệnh
        public int VisitId { get; set; } // Khóa ngoại đến lịch hẹn khám bệnh
        public int PatientId { get; set; } // Khóa ngoại đến bệnh nhân
        public int ProviderId { get; set; } // Khóa ngoại đến bác sĩ
        public Visit Visit { get; set; }
        public Patient Patient { get; set; }
        public Employee Provider { get; set; }  
    }
}
