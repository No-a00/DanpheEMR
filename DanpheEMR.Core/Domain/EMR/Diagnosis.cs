using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.ADTModels;
using DanpheEMR.Core.Domain.Base;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DanpheEMR.Core.Domain.Appointment
{
    public class Diagnosis : BaseEntity
    {
        public int Id { get; set; }
        public DateTime DiagnosisDate { get; set; } = DateTime.Now;
        public string ICD10Code { get; set; } // Mã ICD-10
        public string Description { get; set; } // Mô tả chẩn đoán
        public bool IsPrimary { get; set; } // Chẩn đoán chính hay phụ
        public int VisitId { get; set; } // Khóa ngoại đến Visit
        public Visit Visit { get; set; } // Navigation property đến Visit
        public int PatirentId { get; set; } // Khóa ngoại đến Patient
        public Patient Patient { get; set; } // Navigation property đến Patient

        public int ProviderId { get; set; } // Khóa ngoại đến Provider (Bác sĩ)
        public Employee Provider { get; set; } // Navigation property đến Provider
    }
}
