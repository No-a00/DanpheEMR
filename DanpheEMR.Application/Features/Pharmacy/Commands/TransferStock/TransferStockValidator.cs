using FluentValidation;

namespace DanpheEMR.Application.Features.Pharmacy.Commands.TransferStock
{
    public class TransferStockValidator : AbstractValidator<TransferStockCommand>
    {
        public TransferStockValidator()
        {
            RuleFor(x => x.FromStoreId).NotEmpty();
            RuleFor(x => x.ToStoreId).NotEmpty();
            RuleFor(x => x).Must(x => x.FromStoreId != x.ToStoreId).WithMessage("Kho xuất và Kho nhập phải khác nhau.");
            RuleFor(x => x.Items).NotEmpty().WithMessage("Vui lòng chọn thuốc cần chuyển.");
        }
    }
}