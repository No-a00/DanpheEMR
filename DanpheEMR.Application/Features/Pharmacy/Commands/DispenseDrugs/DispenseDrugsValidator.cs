using FluentValidation;

namespace DanpheEMR.Application.Features.Pharmacy.Commands.DispenseDrugs
{
    public class DispenseDrugsValidator : AbstractValidator<DispenseDrugsCommand>
    {
        public DispenseDrugsValidator()
        {
            RuleFor(x => x.StoreId).NotEmpty().WithMessage("Vui lòng chọn quầy thuốc xuất.");
            RuleFor(x => x.Items).NotEmpty().WithMessage("Đơn thuốc trống.");
        }
    }
}