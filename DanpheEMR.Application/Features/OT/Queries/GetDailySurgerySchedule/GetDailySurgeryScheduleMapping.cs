using AutoMapper;
using DanpheEMR.Core.Domain.OT;

namespace DanpheEMR.Application.Features.OT.Queries.GetDailySurgerySchedule
{
    public class GetDailySurgeryScheduleMapping : Profile
    {
        public GetDailySurgeryScheduleMapping()
        {
            CreateMap<OTSchedule, GetDailySurgeryScheduleResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.OTRoom != null ? src.OTRoom.RoomName : ""))
                .ForMember(dest => dest.SurgeonName, opt => opt.MapFrom(src => src.Surgeon != null ? src.Surgeon.FullName : ""))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient != null ? src.Patient.FirstName + " " + src.Patient.LastName : ""));
        }
    }
}