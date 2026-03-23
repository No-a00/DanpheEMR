namespace DanpheEMR.Core.Domain.Patients
{
    public class PatientAddress
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public Guid patientId { get; set; }
        public Patient Patient { get; set; }
    }
}
