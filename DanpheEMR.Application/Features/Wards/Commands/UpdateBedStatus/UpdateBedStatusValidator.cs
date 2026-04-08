using FluentValidation;

namespace DanpheEMR.Application.Features.Inpatient.Commands.UpdateBedStatus
{
    public class UpdateBedStatusValidator : AbstractValidator<UpdateBedStatusCommand>
    {
        public UpdateBedStatusValidator()
        {
            RuleFor(x => x.BedId).NotEmpty();
            RuleFor(x => x.NewStatus).IsInEnum().WithMessage("Trạng thái giường không hợp lệ.");
        }
    }
}