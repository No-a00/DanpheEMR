using FluentValidation;

namespace DanpheEMR.Application.Features.Pharmacy.Commands.SetupPharmacyItem
{
    public class SetupPharmacyItemValidator : AbstractValidator<SetupPharmacyItemCommand>
    {
        public SetupPharmacyItemValidator()
        {
            RuleFor(x => x.ItemCode).NotEmpty().WithMessage("Mã thuốc không được để trống.");
            RuleFor(x => x.ItemName).NotEmpty().WithMessage("Tên thuốc/Vật tư không được để trống.");
            RuleFor(x => x.UOM).NotEmpty().WithMessage("Đơn vị tính không được để trống.");
        }
    }
}