using  DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Patients
{
    public class Patient : BaseEntity
    {
        public int Id { get; set; }
        public string PatientCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }

        public string PhoneNumber { get; set; }
        public string BloodGroup { get; set; }
        public ICollection<PatientAddress> Addresses { get; set; }
        public ICollection<PatientKin> Kins { get; set; }
        public ICollection<Visit> Visits { get; set; }
        public ICollection<Admission> Admissions { get; set; }
    }
}
