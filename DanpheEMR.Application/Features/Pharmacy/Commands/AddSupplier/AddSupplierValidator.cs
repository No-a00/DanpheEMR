using FluentValidation;

namespace DanpheEMR.Application.Features.Pharmacy.Commands.AddSupplier
{
    public class AddSupplierValidator : AbstractValidator<AddSupplierCommand>
    {
        public AddSupplierValidator()
        {
            RuleFor(x => x.SupplierName).NotEmpty().WithMessage("Tên nhà cung cấp không được để trống.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại không được để trống.");
        }
    }
}