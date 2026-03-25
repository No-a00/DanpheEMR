using AutoMapper;
using DanpheEMR.Core.Domain.Billing;
namespace DanpheEMR.Application.Features.Billing.Commands.GenerateInvoice
{
    public class GenerateInvoiceMapping : Profile
    {
        public GenerateInvoiceMapping()
        {
            CreateMap<BillingTransaction, GenerateInvoiceResponse>();
        }
    }
}
