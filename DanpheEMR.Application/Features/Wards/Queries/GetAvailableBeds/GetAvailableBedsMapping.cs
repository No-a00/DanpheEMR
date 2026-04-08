using AutoMapper;
using DanpheEMR.Core.Domain.Wards;

namespace DanpheEMR.Application.Features.Inpatient.Queries.GetAvailableBeds
{
    public class GetAvailableBedsMapping : Profile
    {
        public GetAvailableBedsMapping()
        {
            CreateMap<Bed, GetAvailableBedsResponse>()
                .ForMember(dest => dest.WardName, opt => opt.MapFrom(src => src.Ward != null ? src.Ward.WardName : ""));
        }
    }
}