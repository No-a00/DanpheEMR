

namespace DanpheEMR.Application.Features.BloodBank.Queries.GetEligibleDonors
{
    public class GetEligibleDonorsResponse
    {
        public int TotalDonors { get; set; }
        public List<EligibleDonorDto> Donors { get; set; } = new();
    }

    public class EligibleDonorDto
    {
        public Guid DonorId { get; set; }
        public string DonorName { get; set; }
        public string Contact { get; set; }
        public string BloodGroupName { get; set; }
        public DateTime? LastDonatedDate { get; set; }
        public int TotalDonations { get; set; }
        public int Age { get; set; }
    }
}