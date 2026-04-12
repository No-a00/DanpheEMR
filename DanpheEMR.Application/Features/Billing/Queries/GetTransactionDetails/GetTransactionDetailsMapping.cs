using AutoMapper;
using DanpheEMR.Core.Domain.Billing;
// using DanpheEMR.Core.Domain.Billing; // Nhớ using đường dẫn tới các Entity thật của bạn (Transaction, TransactionItem)

namespace DanpheEMR.Application.Features.Billing.Queries.GetTransactionDetails
{
    public class GetTransactionDetailsMapping : Profile
    {
        public GetTransactionDetailsMapping()
        {

            CreateMap<BillingTransaction, GetTransactionDetailsResponse>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FullName)) 
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.TransactionItems));
            CreateMap<BillingTransactionItem, TransactionItemDto>()
                .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.ServiceItem.ItemName))
                .ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.Quantity * src.Price));
            
        }
    }
}