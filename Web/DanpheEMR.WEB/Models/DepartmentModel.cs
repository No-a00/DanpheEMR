using System.ComponentModel.DataAnnotations;

namespace DanpheEMR.WEB.Models
{
    public class DepartmentModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } // Tên khoa

        [MaxLength(500)]
        public string Description { get; set; } // Mô tả chức năng của khoa

        // Navigation Properties: 1 Khoa có nhiều Bác sĩ và nhiều Bệnh nhân
        public virtual ICollection<DoctorModel> Doctors { get; set; }
        public virtual ICollection<PatientModel> Patients { get; set; }

        public DepartmentModel()
        {
            Doctors = new HashSet<DoctorModel>();
            Patients = new HashSet<PatientModel>();
        }
    }
}