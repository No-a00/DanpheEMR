using MediatR;


namespace DanpheEMR.Application.Features.BloodBank.Commands.RegisterDonor
{
    public class RegisterDonorCommand : IRequest<Result<RegisterDonorResponse>>
    {
        public string DonorName { get; set; }
        public string Contact { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public float Weight { get; set; }
        public Guid BloodGroupId { get; set; }
    }
}