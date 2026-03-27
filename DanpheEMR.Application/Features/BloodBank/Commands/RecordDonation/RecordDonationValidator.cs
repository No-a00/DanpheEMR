using FluentValidation;

namespace DanpheEMR.Application.Features.BloodBank.Commands.RecordDonation
{
    public class RecordDonationValidator : AbstractValidator<RecordDonationCommand>
    {
        public RecordDonationValidator()
        {
            RuleFor(x => x.DonorId)
                .NotEmpty().WithMessage("Vui lòng chọn người hiến máu.");

            RuleFor(x => x.BagNumber)
                .NotEmpty().WithMessage("Mã bịch máu không được để trống.")
                .MaximumLength(50).WithMessage("Mã bịch máu không được vượt quá 50 ký tự.");

            RuleFor(x => x.VolumeInMl)
                .GreaterThanOrEqualTo(250).WithMessage("Thể tích máu hiến tối thiểu thường là 250ml.")
                .LessThanOrEqualTo(500).WithMessage("Thể tích máu hiến không hợp lý (quá lớn).");
        }
    }
}