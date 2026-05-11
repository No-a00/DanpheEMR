using AutoMapper;
using DanpheEMR.Core.Domain.Wards;

namespace DanpheEMR.Application.Features.Inpatient.Commands.SetupWard
{
    public class SetupWardMapping : Profile
    {
        public SetupWardMapping()
        {
            CreateMap<SetupWardCommand, Ward>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.TotalBeds, opt => opt.MapFrom(src => 1000));
        }
    }
}