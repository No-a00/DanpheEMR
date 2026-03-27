
using DomainBloodDonor = DanpheEMR.Core.Domain.BloodBank.BloodDonor;

namespace DanpheEMR.Application.Features.BloodBank.Queries.GetEligibleDonors
{
    public static class GetEligibleDonorsMapping
    {
        public static EligibleDonorDto ToDto(this DomainBloodDonor donor)
        {
            return new EligibleDonorDto
            {
                DonorId = donor.Id,
                DonorName = donor.DonorName,
                Contact = donor.Contact,
                BloodGroupName = donor.BloodGroup != null ? donor.BloodGroup.BloodGroupName.ToString() : "N/A",
                LastDonatedDate = donor.LastDonatedDate,
                TotalDonations = donor.TotalDonations,

               
                Age = DateTime.Today.Year - donor.DateOfBirth.Year -
                     (donor.DateOfBirth.Date > DateTime.Today.AddYears(-(DateTime.Today.Year - donor.DateOfBirth.Year)) ? 1 : 0)
            };
        }

        public static List<EligibleDonorDto> ToDtoList(this IEnumerable<DomainBloodDonor> donors)
        {
            return donors.Select(d => d.ToDto()).ToList();
        }
    }
}