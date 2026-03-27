
using MediatR;


namespace DanpheEMR.Application.Features.BloodBank.Queries.GetEligibleDonors
{
    public record GetEligibleDonorsQuery(
        Guid? BloodGroupId 
    ) : IRequest<Result<GetEligibleDonorsResponse>>;
}