using AutoMapper;
using DanpheEMR.Core.Domain.Wards;
using DanpheEMR.Core.Enums; 


namespace DanpheEMR.Application.Features.Inpatient.Commands.AddBed
{
    public class AddBedMapping : Profile
    {
        public AddBedMapping()
        {
            CreateMap<AddBedCommand, Bed>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => BedStatus.Available));
        }
    }
}