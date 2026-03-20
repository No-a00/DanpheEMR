using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;


namespace DanpheEMR.Core.Domain.EMR
{
    public class Diagnosis : BaseEntity
    {
        public int Id { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string ICD10Code { get; set; } // Mã ICD-10
        public string Description { get; set; } // Mô tả chẩn đoán
        public bool IsPrimary { get; set; } // Chẩn đoán chính hay phụ
        // hủy 
        public string reason { get; set; }
        public bool isDelete { get; set; }
        public int VoidedBy { get; set; }
        //
         public int VisitId { get; set; } // Khóa ngoại đến Visit
        public Visit Visit { get; set; } // Navigation property đến Visit
        public int PatientId { get; set; } // Khóa ngoại đến Patient
        public Patient Patient { get; set; } // Navigation property đến Patient

        public int ProviderId { get; set; } // Khóa ngoại đến Provider (Bác sĩ)
        public Employee Provider { get; set; } // Navigation property đến Provider
    }
}
