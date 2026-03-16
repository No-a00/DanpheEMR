namespace DanpheEMR.Core.Domain.ADTModels
{
    public class PatientAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int patientId { get; set; }
        public Patient Patient { get; set; }
    }
}
