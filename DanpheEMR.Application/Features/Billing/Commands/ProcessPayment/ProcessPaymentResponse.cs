using Application.Common.Enums;

namespace DanpheEMR.Application.Features.Billing.Commands.ProcessPayment
{
    public class ProcessPaymentResponse
    {
        public Guid PaymentId;
        public string NewInvoiceStatus;
    }
}
