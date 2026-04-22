using DanpheEMR.Core.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanpheEMR.Core.Domain.Patients
{
    public class Patient : BaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }
        public string PatientCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{LastName} {FirstName}".Trim();
            }
        }

        public DateTime DOB { get; set; }
        public string Gender { get; set; }

        public string PhoneNumber { get; set; }
        public string IdCardNumber { get; set; }
        public string BloodGroup { get; set; }

        // Thông tin xóa mềm
        public bool IsDeleted { get; set; }

        public string? Reason { get; set; }
        public Guid? DeletedBy { get; set; }

        public ICollection<PatientAddress> Addresses { get; set; }
        public ICollection<PatientKin> Kins { get; set; }
        public ICollection<Visit> Visits { get; set; }
        public ICollection<Admission> Admissions { get; set; }
    }
}