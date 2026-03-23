using AutoMapper;
namespace DanpheEMR.Application.Features.Billing.Commands.GenerateInvoice
{
    public class GenerateInvoiceMapping : Profile
    {
        public GenerateInvoiceMapping()
        {
            CreateMap<Invoice, GenerateInvoiceResponse>();
        }
    }
}
