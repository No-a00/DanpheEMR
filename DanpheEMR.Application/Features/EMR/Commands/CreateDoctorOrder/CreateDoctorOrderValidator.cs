using FluentValidation;

namespace DanpheEMR.Application.Features.EMR.Commands.CreateDoctorOrder
{
    public class CreateDoctorOrderValidator : AbstractValidator<CreateDoctorOrderCommand>
    {
        public CreateDoctorOrderValidator()
        {
            RuleFor(x => x.VisitId).NotEmpty().WithMessage("Lượt khám không được để trống.");
            RuleFor(x => x.ProviderId).NotEmpty().WithMessage("Bác sĩ ra y lệnh không được để trống.");

            RuleFor(x => x.OrderText)
                .NotEmpty().WithMessage("Nội dung y lệnh không được để trống.");
        }
    }
}