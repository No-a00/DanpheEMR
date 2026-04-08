using FluentValidation;

namespace DanpheEMR.Application.Features.Pharmacy.Commands.ReceiveGoods
{
    public class ReceiveGoodsValidator : AbstractValidator<ReceiveGoodsCommand>
    {
        public ReceiveGoodsValidator()
        {
            RuleFor(x => x.SupplierId).NotEmpty().WithMessage("Vui lòng chọn Nhà cung cấp.");
            RuleFor(x => x.Items).NotEmpty().WithMessage("Phiếu nhập kho phải có ít nhất 1 mặt hàng.");
        }
    }
}