
namespace DanpheEMR.Application.Features.Billing.Commands.GenerateInvoice
{
    public class GenerateInvoiceResponse
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
