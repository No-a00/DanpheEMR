
using MediatR;

namespace DanpheEMR.Application.Features.Billing.Commands.ProcessPayment
{
    public record class ProcessPaymentCommand(Guid InvoiceId,Guid VisitId,Guid PatientId, decimal AmountPaid ,string PaymentMethod) :IRequest<Result<ProcessPaymentResponse>>
    {
    }
}
