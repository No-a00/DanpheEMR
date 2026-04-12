using FluentValidation;

namespace DanpheEMR.Application.Features.Billing.Queries.GetTransactionDetails
{
    public class GetTransactionDetailsQueryValidator : AbstractValidator<GetTransactionDetailsQuery>
    {
        public GetTransactionDetailsQueryValidator()
        {
            RuleFor(x => x.TransactionId)
                .NotEmpty().WithMessage("Mã giao dịch (TransactionId) không được để trống.");
        }
    }
}