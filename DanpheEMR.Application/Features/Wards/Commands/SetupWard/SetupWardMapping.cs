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
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
        }
    }
}