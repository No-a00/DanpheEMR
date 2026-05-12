using DanpheEMR.Core.Domain.Admin;
using DanpheEMR.Core.Domain.Base;
using DanpheEMR.Core.Domain.Patients;
using System.ComponentModel.DataAnnotations;

namespace DanpheEMR.Core.Domain.EMR
{
    public class MedicationAdministration : BaseEntity,ISoftDelete
    {
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        public DateTime AdministeredTime { get; set; }
        public string DosageGiven { get; set; }// Liều lượng đã cho

        public string Remarks { get; set; }// Ghi chú về việc cho thuốc, ví dụ: "Đã cho thuốc sau bữa ăn", "Bệnh nhân có phản ứng phụ nhẹ", v.v.
        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }
       public string? DeletedBy { get; set; }

        public Guid AdmissionId { get; set; }
        public Admission Admission { get; set; }
        public Guid PrescriptionItemId { get; set; }
        public PrescriptionItem PrescriptionItem { get; set; }
        public Guid NurseId { get; set; }
        public Employee Nurse { get; set; }

    }
}
