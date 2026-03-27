using AutoMapper;
namespace DanpheEMR.Application.Features.Appointments.Queries.GetDoctorSchedule
{
    public sealed class GetDoctorScheduleMapping : Profile
    {
        public GetDoctorScheduleMapping()
        {
            CreateMap<DoctorSchedule, DoctorScheduleResponse>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToString(@"hh\:mm")))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToString(@"hh\:mm")));
        }
    }
}