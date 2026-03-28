using DanpheEMR.Core.Domain.Base;
namespace DanpheEMR.Core.Domain.Patients
{
    public class PatientAddress : BaseEntity
    {
        public Guid Id { get; set; }

        public string AddressType { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; }
    }
}