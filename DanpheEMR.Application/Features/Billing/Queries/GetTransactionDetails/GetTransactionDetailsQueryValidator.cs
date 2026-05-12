using FluentValidation;

namespace DanpheEMR.Application.Features.Billing.Queries.GetTransactionDetails
{
    public class GetTransactionDetailsQueryValidator : AbstractValidator<GetTransactionDetailsQuery>
    {
        public GetTransactionDetailsQueryValidator()
        {
            RuleFor(x => x.TransactionCode)
                .NotEmpty().WithMessage("Mã giao dịch (TransactionCode) không được để trống.");
        }
    }
}