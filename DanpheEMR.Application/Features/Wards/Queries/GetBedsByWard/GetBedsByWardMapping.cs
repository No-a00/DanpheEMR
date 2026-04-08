using AutoMapper;
using DanpheEMR.Core.Domain.Wards;

namespace DanpheEMR.Application.Features.Inpatient.Queries.GetBedsByWard
{
    public class GetBedsByWardMapping : Profile
    {
        public GetBedsByWardMapping()
        {
            CreateMap<Bed, GetBedsByWardResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}