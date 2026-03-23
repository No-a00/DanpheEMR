using FluentValidation;
namespace DanpheEMR.Application.Features.Billing.Commands.GenerateInvoice
{
    public class GenerateInvoiceValidator : AbstractValidator<GenerateInvoiceCommand>
    {
        public GenerateInvoiceValidator()
        {
            RuleFor(x => x.VisitId).NotEmpty().WithMessage("Mã lượt khám không được để trống.");
            RuleFor(x => x.Items).NotEmpty().WithMessage("Hóa đơn phải có ít nhất một dịch vụ.");
            RuleForEach(x => x.Items).ChildRules(item => {
                item.RuleFor(i => i.Quantity).GreaterThan(0);
                item.RuleFor(i => i.UnitPrice).GreaterThanOrEqualTo(0);
            });
        }
    }
}
