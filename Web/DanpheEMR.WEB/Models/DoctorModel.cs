using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanpheEMR.WEB.Models
{
    public class DoctorModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(100)]
        public string Specialization { get; set; } // Chuyên khoa

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }
        public bool IsAvailable { get; set; } = true; // Trạng thái sẵn sàng nhận bệnh nhân mới

        // Khóa ngoại: Bác sĩ trực thuộc khoa nào
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual DepartmentModel Department { get; set; }

        // Navigation Properties: 1 Bác sĩ phụ trách nhiều Bệnh nhân và tạo nhiều Bệnh án
        public virtual ICollection<PatientModel> AssignedPatients { get; set; }
        public virtual ICollection<MedicalHistoryModel> MedicalHistories { get; set; }

        public DoctorModel()
        {
            AssignedPatients = new HashSet<PatientModel>();
            MedicalHistories = new HashSet<MedicalHistoryModel>();
        }
    }
}