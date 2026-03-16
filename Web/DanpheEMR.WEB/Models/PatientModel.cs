using DanpheEMR.WEB.Iterface.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanpheEMR.WEB.Models
{
    public class PatientModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; } 

        [MaxLength(20)]
        public string NationalId { get; set; } 

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string EmergencyContact { get; set; } // Người liên hệ khẩn cấp
        public PatientStatus Status { get; set; } = PatientStatus.New;

        public DateTime? AdmissionDate { get; set; } // Ngày nhập viện (AdmitPatientAsync)
        public DateTime? DischargeDate { get; set; } // Ngày xuất viện (DischargePatientAsync)

        // 3. LIÊN KẾT DỮ LIỆU (Foreign Keys & Navigation Properties)

        // Bác sĩ đang phụ trách (AssignDoctorToPatientAsync)
        public int? AssignedDoctorId { get; set; }
        [ForeignKey("AssignedDoctorId")]
        public virtual DoctorModel AssignedDoctor { get; set; }

        // Khoa đang nằm điều trị (TransferPatientAsync)
        public int? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual DepartmentModel Department { get; set; }

        // Lịch sử khám bệnh (GetPatientMedicalHistoryAsync)
        // Một bệnh nhân có thể có nhiều lần khám/nhiều hồ sơ bệnh án
        public virtual ICollection<MedicalHistoryModel> MedicalHistories { get; set; }

        // Hàm khởi tạo để tránh lỗi NullReferenceException khi dùng ICollection
        public PatientModel()
        {
            MedicalHistories = new HashSet<MedicalHistoryModel>();
        }
    }
}