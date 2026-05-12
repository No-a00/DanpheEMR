using FluentValidation;
namespace DanpheEMR.Application.Features.Billing.Commands.ProcessPayment
{
    public class ProcessPaymentValidator : AbstractValidator<ProcessPaymentCommand>
    {
        public ProcessPaymentValidator()
        {
            RuleFor(x => x.InvoiceCode).NotEmpty().WithMessage("Mã hóa đơn không được để trống.");
            RuleFor(x => x.AmountPaid).GreaterThan(0).WithMessage("Số tiền thanh toán phải lớn hơn 0.");
            RuleFor(x => x.PaymentMethod).NotEmpty();
        }
    }
}
