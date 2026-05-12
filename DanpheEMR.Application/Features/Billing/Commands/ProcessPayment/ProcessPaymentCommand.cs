
using MediatR;

namespace DanpheEMR.Application.Features.Billing.Commands.ProcessPayment
{
    public record class ProcessPaymentCommand(string InvoiceCode,string VisitCode,string PatientCode, decimal AmountPaid ,string PaymentMethod) :IRequest<Result<ProcessPaymentResponse>>
    {
    }
}
