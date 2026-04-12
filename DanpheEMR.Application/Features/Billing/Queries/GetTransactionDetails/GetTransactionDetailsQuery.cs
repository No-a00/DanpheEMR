
using MediatR;

namespace DanpheEMR.Application.Features.Billing.Queries.GetTransactionDetails
{
    public class GetTransactionDetailsQuery : IRequest<Result<GetTransactionDetailsResponse>>
    {
        public Guid TransactionId { get; set; }

        public GetTransactionDetailsQuery(Guid transactionId)
        {
            TransactionId = transactionId;
        }
    }
}