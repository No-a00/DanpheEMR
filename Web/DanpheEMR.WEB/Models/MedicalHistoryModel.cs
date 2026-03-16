using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanpheEMR.WEB.Models
{
    public class MedicalHistoryModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime VisitDate { get; set; } = DateTime.Now; // Ngày giờ khám

        [Required, MaxLength(255)]
        public string? Symptoms { get; set; } // Triệu chứng lâm sàng

        [Required, MaxLength(255)]
        public string? Diagnosis { get; set; } // Chẩn đoán của bác sĩ

        public string? TreatmentPlan { get; set; } // Phác đồ điều trị / Đơn thuốc

        public string? Notes { get; set; } // Ghi chú thêm

        // Khóa ngoại: Bệnh án này của Bệnh nhân nào?
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientModel Patient { get; set; }

        // Khóa ngoại: Bác sĩ nào thực hiện khám?
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual DoctorModel Doctor { get; set; }
    }
}