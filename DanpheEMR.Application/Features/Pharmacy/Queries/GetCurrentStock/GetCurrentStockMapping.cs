using AutoMapper;
using DanpheEMR.Core.Domain.Pharmacy; 
namespace DanpheEMR.Application.Features.Pharmacy.Queries.GetCurrentStock
{
    public class GetCurrentStockMapping : Profile
    {
        public GetCurrentStockMapping()
        {
            CreateMap<Stock, GetCurrentStockResponse>()
                .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item != null ? src.Item.ItemName : ""))
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store != null ? src.Store.Name : ""));
        }
    }
}