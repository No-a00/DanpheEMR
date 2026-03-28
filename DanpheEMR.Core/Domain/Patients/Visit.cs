using DanpheEMR.Core.Enums;
using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.EMR;

namespace DanpheEMR.Core.Domain.Patients
{
    public class Visit : BaseEntity, IHasActiveStatus
    {
        public Guid Id { get; set; }
        public string VisitCode { get; set; } // Mã lượt khám (Dùng để tạo Barcode/QR Code)
        public Guid? QueueNo { get; set; } // Số thứ tự chờ khám
        public string ChiefComplaint { get; set; } // Lý do đến khám (Đau bụng, Sốt...)

        public string VisitType { get; set; }
        public DateTime VisitDate { get; set; }
        public VisitStatus Status { get; set; }
        public bool IsActive { get; set; }
        public string CancelReason { get; set; }
        public Guid UserIdCancel { get; set; }   

        public Guid PatientId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid ProviderId { get; set; }

        public Patient Patient { get; set; }
        public Department Department { get; set; }
        public Employee Provider { get; set; }

        public ICollection<ClinicalNote> ClinicalNotes { get; set; }
        public ICollection<Diagnosis> Diagnoses { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
        public ICollection<Vitals> Vitals { get; set; } // Liên kết đến Sinh hiệu
        public ICollection<DoctorOrder> DoctorOrders { get; set; } // Liên kết đến Chỉ định Cận lâm sàng
        public ICollection<Admission> Admissions { get; set; } // Liên kết đến Nội trú (nếu có)
    }
}