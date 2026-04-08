using AutoMapper;
using DanpheEMR.Core.Domain.Pharmacy; 

namespace DanpheEMR.Application.Features.Pharmacy.Commands.AddSupplier
{
    public class AddSupplierMapping : Profile
    {
        public AddSupplierMapping()
        {
            CreateMap<AddSupplierCommand, Supplier>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
        }
    }
}