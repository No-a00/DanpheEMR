using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Appointment;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Nums;

namespace DanpheEMR.Core.Domain.ADTModels
{
    public class Visit : BaseEntity
    {
        public int Id { get; set; }
        public string VisitType { get; set; } // Loại khám: Khám thường, Khám cấp cứu, Khám chuyên khoa, v.v.
        public DateTime VisitDate { get; set; } // Ngày giờ khám
        public VisitStatus Status { get; set; } // Trạng thái: Đang khám, Đã hoàn thành, Đã hủy, v.v.
        public int  PatientId { get; set; } // Khóa ngoại đến Bệnh nhân
        public int DepartmentId { get; set; }
        public int ProviderId { get; set; } // Khóa ngoại đến Bác sĩ phụ trách
        public Patient Patient { get; set; } // Navigation property đến Bệnh nhân
        public Department Department { get; set; } // Navigation property đến Khoa
        public Employee Provider { get; set; } // Navigation property đến Bác sĩ phụ trách
        public ICollection<ClinicalNote> ClinicalNotes { get; set; } // Các ghi chú lâm sàng liên quan đến lần khám này
        public ICollection<Diagnosis> Diagnoses { get; set; } // Các chẩn đoán liên quan đến lần khám này
        public ICollection<Prescription> Prescriptions { get; set; } // Các đơn thuốc liên quan đến lần khám này
    }
}
