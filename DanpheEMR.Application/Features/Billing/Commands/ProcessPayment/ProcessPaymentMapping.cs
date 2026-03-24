
using AutoMapper;
using DanpheEMR.Core.Domain.Billing;

namespace DanpheEMR.Application.Features.Billing.Commands.ProcessPayment
{
    public class ProcessPaymentMapping : Profile
    {
        public ProcessPaymentMapping()
        {
            CreateMap<BillingTransaction, ProcessPaymentResponse>();
        }
    }
}
