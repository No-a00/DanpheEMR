
using MediatR;

namespace DanpheEMR.Application.Features.Billing.Queries.GetTransactionDetails
{
    public class GetTransactionDetailsQuery : IRequest<Result<GetTransactionDetailsResponse>>
    {
        public string TransactionCode { get; set; }

        public GetTransactionDetailsQuery(string transactionCode)
        {
            TransactionCode = transactionCode;
        }
    }
}